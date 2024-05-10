using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    /// <summary>
    /// Always optional. Groups are aggregators for users within a campaign.
    /// For example, a group can be a Player on a sport's team and the users would be the people supporting them.
    /// </summary>
    public class GroupData
    {
        /// <summary>
        /// required. A string with the group's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// optional. A sport's player number on the team.
        /// </summary>
        [JsonPropertyName("player_number")]
        public string? PlayerNumber { get; set; }
    }
}
