using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    public class Payload
    {
        [JsonPropertyName("user_data")]
        public UserData UserData { get; set; }
        [JsonPropertyName("campaign_data")]
        public CampaignData CampaignData { get; set; }
        [JsonPropertyName("group_data")]
        public GroupData? GroupData { get; set; }
        [JsonPropertyName("organization_data")]
        public OrganizationData? OrganizationData { get; set; }
        [JsonPropertyName("utm_data")]
        public UtmData? UtmData { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("expires")]
        [JsonIgnore]
        public int Expires { get; set; }
    }
}
