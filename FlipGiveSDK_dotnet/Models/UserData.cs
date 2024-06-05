using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    /// <summary>
    /// Required when campaign_data is not present in the payload, otherwise optional.
    /// It represents the user using the Shop, and contains the following information
    /// 
    /// Optional fields of invalid formats will not be validated but will be ignored.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// required. A string representing the user's ID in your system.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// required. A string with the user's email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
        /// <summary>
        /// required. A string with the user's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// required. A string with the ISO code of the user's country, which must be 'CAN' or 'USA' at this time.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }
        /// <summary>
        /// optional. A string with the user's city.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }
        /// <summary>
        /// optional. A string with the user's state. It must be a 2 letter code.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }
        /// <summary>
        /// optional. A string with the user's postal code. It must match Regex /\d{5}/ for the USA or /[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d/ for Canada.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
        /// <summary>
        /// optional. A float with the user's latitude in decimal degree format. Without accompanying :longitude, latitude will be ignored.
        /// </summary>
        [JsonPropertyName("latitude")]
        public float? Latitude { get; set; }
        /// <summary>
        /// optional. A float with the user's longitude in decimal degree format. Without accompanying :latitude, longitude will be ignored.
        /// </summary>
        [JsonPropertyName("longitude")]
        public float? Longitude { get; set; }
        /// <summary>
        /// optional. A string containing the URL for the user's avatar.
        /// </summary>
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

    }
}
