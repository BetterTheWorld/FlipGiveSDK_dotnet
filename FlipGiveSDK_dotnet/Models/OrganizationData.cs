using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Models
{
    /// <summary>
    /// Always optional. Organizations are used to group campaigns.
    /// As an example: A School (organization) has many Grades (campaigns), with Students (groups) and Parents (users) shopping to support their student.
    /// </summary>
    public class OrganizationData
    {
        /// <summary>
        /// required. A string with the organization's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// required. A string with the organization's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// required. The user information for the organization's admin. It must contain the same information as user_data
        /// </summary>
        [JsonPropertyName("admin_data")]
        public UserData AdminData { get; set; }
    }
}
