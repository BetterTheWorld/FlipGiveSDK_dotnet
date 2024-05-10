using FlipGiveSDK_dotnet.Exceptions;
using FlipGiveSDK_dotnet.Exceptions.PayloadExceptions;
using FlipGiveSDK_dotnet.Helpers;
using FlipGiveSDK_dotnet.Models;
using FlipGiveSDK_dotnet.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlipGiveSDK_dotnet
{
    /// <summary>
    /// The main purpose of FlipGiveRewardsService is to generate Tokens to gain access to FlipGive's Rewards API. There are 6 methods available on the service.
    /// </summary>
    public class FlipGiveRewardsService
    {
        /// <summary>
        /// JweHelper to encrypt and decrypt
        /// </summary>
        private readonly JweHelper _jweHelper;
        /// <summary>
        /// The shop id provided by FlipGive
        /// </summary>
        private readonly string _cloudShopId;
        /// <summary>
        /// The countries where FlipGive is available
        /// </summary>
        private readonly string[] COUNTRIES = { "CAN", "USA" };

        /// <summary>
        /// Constructor of Service using Options pattern
        /// </summary>
        /// <param name="options">Options to create the service, it needs the shopId and also the secret</param>
        public FlipGiveRewardsService(IOptions<FlipGiveRewardsOptions> options)
        {
            _jweHelper = new JweHelper(options.Value.Secret.Replace("sk_", ""));
            _cloudShopId = options.Value.CloudShopId;
        }

        /// <summary>
        /// This method is used to decode a token that has been generated with your credentials. It takes a single string as an argument and, if able to decode the token, it will return a Payload.
        /// </summary>
        /// <param name="token">The token encrypted with your credentials</param>
        /// <returns>A valid Payload</returns>
        /// <exception cref="InvalidTokenException">Throws when null token or when it doesn't have both parts hash@cloudShopId</exception>
        public Payload? ReadToken(string token)
        {
            if(token == null)
            {
                throw new InvalidTokenException("Null Token");
            }

            var splitToken = token.Split('@');

            if (splitToken == null || splitToken.Length < 2 || splitToken[1] != _cloudShopId)
            {
                throw new InvalidTokenException("Token not formed with 2 keys token@cloudShopId");
            }

            return _jweHelper.Decrypt(splitToken[0]);
        }

        /// <summary>
        /// This method is used to generate a token that will identify a user or campaign. It accepts a Payload as an argument it validates it and it returns an encrypted token.
        /// </summary>
        /// <param name="payload">The valid payload that will be encrypted</param>
        /// <returns>A generated token that will identify a user or campaign</returns>
        public string IdentifiedToken(Payload payload)
        {
            IsValidIdentified(payload);

            var token = _jweHelper.Encrypt(payload);

            return $"{token}@{_cloudShopId}";
        }

        /// <summary>
        /// This method is used to generate a token that can only be used by the Rewards partner (that's you) to access reports and other API endpoints. It is only valid for an hour.
        /// </summary>
        /// <returns>A token that is only valid for an hour</returns>
        public string GetPartnerToken()
        {
            var createdAt = DateTimeOffset.UtcNow;

            var payload = new { type = "partner", created_at = createdAt.ToUnixTimeSeconds() };

            var token = _jweHelper.Encrypt(payload);

            return $"{token}@{_cloudShopId}";
        }

        /// <summary>
        /// This method is used to validate a payload, without attempting to generate a token. It returns a Boolean. The same rules for IdentifiedToken apply here as well.
        /// </summary>
        /// <param name="payload">Payload to be validated</param>
        /// <returns>True if the Payload is valid, false if the Payload is not valid</returns>
        public bool IsValidIdentified(Payload payload)
        {
            ValidatePayload(payload);

            if (payload.UserData != null)
            {
                ValidatePersonData(payload.UserData, "user_data");
            }

            if (payload.CampaignData != null)
            {
                ValidateCampaignData(payload.CampaignData, "campaign_data");
            }

            if (payload.GroupData != null)
            {
                ValidateGroupData(payload.GroupData, "group_data");
            }

            if (payload.OrganizationData != null)
            {
                ValidateOrganizationData(payload.OrganizationData, "organization_data");
            }

            return true;
        }

        /// <summary>
        /// This method is used to validate the structure of the payload and minimum data of the payload
        /// </summary>
        /// <param name="payload">Payload to be validated</param>
        private void ValidatePayload(Payload payload)
        {
            ValidateFormat(payload);
            ValidateMinimumData(payload);
        }

        /// <summary>
        /// This method is used to validate the structure of the payload.
        /// </summary>
        /// <param name="payload">Payload to be validated</param>
        /// <exception cref="MissingPayloadException">When payload is null</exception>
        private void ValidateFormat(object payload)
        {
            if(payload == null)
            {
                throw new MissingPayloadException("Payload is null");
            }
        }

        /// <summary>
        /// This method is used to validate the minimum data of the payload
        /// </summary>
        /// <param name="payload">Payload to be validated</param>
        /// <exception cref="MinimumPayloadException">When payload doesn't have one of the user_data or campaign_data</exception>
        private void ValidateMinimumData(Payload payload)
        {
            if (payload.UserData == null && payload.CampaignData == null)
            {
                throw new MinimumPayloadException("Payload doesn't contain minimal data: user_data or campaign_data");
            }
        }

        /// <summary>
        /// This method is used to validate a UserData
        /// </summary>
        /// <param name="userData">UserData to be validated</param>
        /// <param name="jsonProperty">JsonProperty name for the serialized payload</param>
        private void ValidatePersonData(UserData userData, string jsonProperty)
        {
            ValidatePresence("id", jsonProperty, userData.Id);
            ValidatePresence("name", jsonProperty, userData.Name);
            ValidatePresence("email", jsonProperty, userData.Email);
            ValidateInclusion("country", jsonProperty, userData.Country, COUNTRIES);
        }

        /// <summary>
        /// This method is used to validate a CampaignData
        /// </summary>
        /// <param name="campaignData">CampaignData to be validated</param>
        /// <param name="jsonProperty">JsonProperty name for the serialized payload</param>
        private void ValidateCampaignData(CampaignData campaignData, string jsonProperty)
        {
            ValidatePresence("id", jsonProperty, campaignData.Id);
            ValidatePresence("name", jsonProperty, campaignData.Name);
            ValidatePresence("category", jsonProperty, campaignData.Category);
            ValidateInclusion("country", jsonProperty, campaignData.Country, COUNTRIES);
            ValidatePersonData(campaignData.AdminData, "campaign_admin_data");
        }

        /// <summary>
        /// This method is used to validate a GroupData
        /// </summary>
        /// <param name="groupData">GroupData to be validated</param>
        /// <param name="jsonProperty">JsonProperty name for the serialized payload</param>
        private void ValidateGroupData(GroupData groupData, string jsonProperty)
        {
            ValidatePresence("name", jsonProperty, groupData.Name);
        }

        /// <summary>
        /// This method is used to validate a OrganizationData
        /// </summary>
        /// <param name="organizationData">OrganizationData to be validated</param>
        /// <param name="jsonProperty">JsonProperty name for the serialized payload</param>
        private void ValidateOrganizationData(OrganizationData organizationData, string jsonProperty)
        {
            ValidatePresence("id", jsonProperty, organizationData.Id);
            ValidatePresence("name", jsonProperty, organizationData.Name);
            ValidatePersonData(organizationData.AdminData, "organization_admin_data");
        }
 
        /// <summary>
        /// This method is used to validate required fields
        /// </summary>
        /// <param name="requiredField">The name of the required field</param>
        /// <param name="property">The name of the property where the field belongs to</param>
        /// <param name="value">The actual value of the required field</param>
        /// <exception cref="RequiredFieldInPayloadMissingException">When the required field it's not present</exception>
        private void ValidatePresence(string requiredField, string property, long value)
        {
            if (value == 0)
            {
                throw new RequiredFieldInPayloadMissingException($"Required field {requiredField} in property {property} is missing");
            }
        }

        /// <summary>
        /// This method is used to validate required fields
        /// </summary>
        /// <param name="requiredField">The name of the required field</param>
        /// <param name="property">The name of the property where the field belongs to</param>
        /// <param name="value">The actual value of the required field</param>
        /// <exception cref="RequiredFieldInPayloadMissingException">When the required field it's not present</exception>
        private void ValidatePresence(string requiredField, string property, string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new RequiredFieldInPayloadMissingException($"Required field {requiredField} in property {property} is missing");
            }
        }

        /// <summary>
        /// This method is used to validate that field it's in inclusion list
        /// </summary>
        /// <param name="requiredField">The name of the required field</param>
        /// <param name="property">The name of the property where the field belongs to</param>
        /// <param name="value">The actual value of the required field</param>
        /// <param name="inclusionList">The list of possible values</param>
        /// <exception cref="RequiredFieldOutsideOfInclusionException">When the required field it's not included in the possible values list</exception>
        private void ValidateInclusion(string requiredField, string property, string value, string[] inclusionList)
        {
            if(string.IsNullOrEmpty(value) || !inclusionList.Select(x => x.ToLower()).Contains(value.ToLowerInvariant()))
            {
                throw new RequiredFieldOutsideOfInclusionException($"Required field {requiredField} in property {property} is not part of: {string.Join(",", inclusionList)}");
            }
        }
    }
}
