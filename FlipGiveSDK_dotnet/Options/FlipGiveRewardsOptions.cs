namespace FlipGiveSDK_dotnet.Options
{
    /// <summary>
    /// Options provided by FlipGive to encrypt and decrypt tokens
    /// </summary>
    public class FlipGiveRewardsOptions
    {
        /// <summary>
        /// The ID of the shop
        /// </summary>
        public string CloudShopId { get; set; }
        /// <summary>
        /// The secret used for encryption
        /// </summary>
        public string Secret {  get; set; }
    }
}
