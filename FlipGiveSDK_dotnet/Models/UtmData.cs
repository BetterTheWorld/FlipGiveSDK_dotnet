using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    /// <summary>
    /// Always optional. UTM data will be saved when a campaign and/or user is created.
    /// </summary>
    public class UtmData
    {
        /// <summary>
        /// A string representing utm_medium.
        /// </summary>
        [JsonPropertyName("utm_medium")]
        public string? UtmMedium { get; set; }
        /// <summary>
        /// A string representing utm_campaign.
        /// </summary>
        [JsonPropertyName("utm_campaign")]
        public string? UtmCampaign { get; set; }
        /// <summary>
        /// A string representing utm_term.
        /// </summary>
        [JsonPropertyName("utm_term")]
        public string? UtmTerm { get; set; }
        /// <summary>
        /// A string representing utm_content.
        /// </summary>
        [JsonPropertyName("utm_content")]
        public string? UtmContent { get; set; }
        /// <summary>
        /// A string representing utm_channel.
        /// </summary>
        [JsonPropertyName("utm_channel")]
        public string? UtmChannel { get; set; }
    }
}
