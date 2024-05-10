using FlipGiveSDK_dotnet;
using FlipGiveSDK_dotnet.Exceptions;
using FlipGiveSDK_dotnet.Helpers;
using FlipGiveSDK_dotnet.Models;
using FlipGiveSDK_dotnet.Options;
using Jose;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Dynamic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Test_FlipGiveSDK_dotnet.Methods
{
    public class SDKGetPartnerTokenTest
    {
        private readonly ServiceProvider _provider;

        public SDKGetPartnerTokenTest()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.UseFlipGiveRewards(config);

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void TestSuccessReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var options = _provider.GetRequiredService<IOptions<FlipGiveRewardsOptions>>();

            #endregion

            #region Act

            var token = flipGiveRewardsService.GetPartnerToken();
            var _jwk = new Jwk(Encoding.UTF8.GetBytes(options.Value.Secret.Replace("sk_", "")));
            var plaintext = JWT.Decrypt(token.Split("@")[0], _jwk);
            var partnerToken = JsonSerializer.Deserialize<JsonObject>(plaintext);

            #endregion

            #region Assert

            Assert.NotNull(token);
            Assert.Equal(options.Value.CloudShopId, token.Split("@")[1]);
            Assert.Equal("partner", (string)partnerToken["type"]);
            Assert.True((long)partnerToken["created_at"] <= DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            #endregion
        }
    }
}