using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    /// <summary>
    /// Required when user_data is not present in the payload, otherwise optional.
    /// It represents the fundraising campaign and contains the following information:
    /// 
    /// Optional fields of invalid formats will not be validated but will be ignored.
    /// </summary>
    public class CampaignData
    {
        /// <summary>
        /// required A string representing the user's ID in your system.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// required A string with the campaign's email.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// required A string with the campaign's category. We will try to match it with one of our existing categories, or assign a default.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }
        /// <summary>
        /// required A string with the ISO code of the campaign's country, which must be 'CAD' or 'USA' at this time.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }
        /// <summary>
        /// required The user information for the campaign's admin. It must contain the same information as user_data
        /// </summary>
        [JsonPropertyName("admin_data")]
        public UserData AdminData { get; set; }
        /// <summary>
        /// optional. A string with the campaign's city.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }
        /// <summary>
        /// optional. A string with the campaign's state. It must be a 2 letter code.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }
        /// <summary>
        /// optional. A string with the campaign's postal code. It must match Regex /\d{5}/ for the USA or /[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d/ for Canada.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
        /// <summary>
        /// optional. A float with the campaign's latitude in decimal degree format.
        /// </summary>
        [JsonPropertyName("latitude")]
        public int? Latitude { get; set; }
        /// <summary>
        /// optional. A float with the campaign's longitude in decimal degree format.
        /// </summary>
        [JsonPropertyName("longitude")]
        public int? Longitude { get; set; }
        /// <summary>
        /// optional. A string containing the URL for the campaign's image, if any.
        /// </summary>
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
