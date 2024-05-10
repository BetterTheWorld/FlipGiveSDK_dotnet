using FlipGiveSDK_dotnet;
using FlipGiveSDK_dotnet.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test_FlipGiveSDK_dotnet
{
    public class SDKExtensionsTest
    {
        private readonly ServiceCollection _services;
        private readonly IConfiguration _config;

        public SDKExtensionsTest()
        {
            _services = new ServiceCollection();

            _config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }

        [Fact]
        public void TestExtensionsWithIConfiguration()
        {
            #region Arrange

            _services.UseFlipGiveRewards(_config);
            var provider = _services.BuildServiceProvider();

            #endregion

            #region Act

            var flipGiveRewardsService = provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTUDgXxMAaOzBHEmlOYFZUuYxNwn6EwzDUbeqP_d7xdRr2tP0fkSoTJusQXaZtfl-6tocMvAGXUxrLIIAAQbSyRGszkCNBbOZSetElTe6FKQwLudRW_VrV4HGlZoNnuCb6CD7xR2kATiTAGPNNcqpe4hDWeCNUAfDHJ3ujsCwuXDbFiD4hinb6nEOiSgIjAaxsZfyQ6qVbIxBrbVRi_6fGMqrKrEQBNckv-Ps-MavLiURe6gX2JQQvC-1wIqdH42EdkW9G7eQWv_hz99GcYf-lX9UUczGK3CpHG1xLwYqKwdswnxdWzwPDRApCabAEcPhHODH2EAo59UGC4g26Gh9RbZdNdkfrvTJfZ9WIiURrgi01OSjG2CkxEbtnMYTkPhfmcMmKMn7IIiRo1zNmHS5xUn2eMpg.t11jsNvC6cNIgx6xHCHtBw@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal(1709224272, payload.UserData.Id);

            #endregion
        }

        [Fact]
        public void TestExtensionsWithActionOptionConfig()
        {
            #region Arrange

            _services.UseFlipGiveRewards(options => _config.GetSection(nameof(FlipGiveRewardsOptions)).Bind(options));
            var provider = _services.BuildServiceProvider();

            #endregion

            #region Act

            var flipGiveRewardsService = provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTUDgXxMAaOzBHEmlOYFZUuYxNwn6EwzDUbeqP_d7xdRr2tP0fkSoTJusQXaZtfl-6tocMvAGXUxrLIIAAQbSyRGszkCNBbOZSetElTe6FKQwLudRW_VrV4HGlZoNnuCb6CD7xR2kATiTAGPNNcqpe4hDWeCNUAfDHJ3ujsCwuXDbFiD4hinb6nEOiSgIjAaxsZfyQ6qVbIxBrbVRi_6fGMqrKrEQBNckv-Ps-MavLiURe6gX2JQQvC-1wIqdH42EdkW9G7eQWv_hz99GcYf-lX9UUczGK3CpHG1xLwYqKwdswnxdWzwPDRApCabAEcPhHODH2EAo59UGC4g26Gh9RbZdNdkfrvTJfZ9WIiURrgi01OSjG2CkxEbtnMYTkPhfmcMmKMn7IIiRo1zNmHS5xUn2eMpg.t11jsNvC6cNIgx6xHCHtBw@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal(1709224272, payload.UserData.Id);

            #endregion
        }

        [Fact]
        public void TestExtensionsWithActionOptionManual()
        {
            #region Arrange

            _services.UseFlipGiveRewards(options =>
            {
                options.CloudShopId = "ABCDEFGH";
                options.Secret = "sk_abcdefghijklmnop";
            });
            var provider = _services.BuildServiceProvider();

            #endregion

            #region Act

            var flipGiveRewardsService = provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTUDgXxMAaOzBHEmlOYFZUuYxNwn6EwzDUbeqP_d7xdRr2tP0fkSoTJusQXaZtfl-6tocMvAGXUxrLIIAAQbSyRGszkCNBbOZSetElTe6FKQwLudRW_VrV4HGlZoNnuCb6CD7xR2kATiTAGPNNcqpe4hDWeCNUAfDHJ3ujsCwuXDbFiD4hinb6nEOiSgIjAaxsZfyQ6qVbIxBrbVRi_6fGMqrKrEQBNckv-Ps-MavLiURe6gX2JQQvC-1wIqdH42EdkW9G7eQWv_hz99GcYf-lX9UUczGK3CpHG1xLwYqKwdswnxdWzwPDRApCabAEcPhHODH2EAo59UGC4g26Gh9RbZdNdkfrvTJfZ9WIiURrgi01OSjG2CkxEbtnMYTkPhfmcMmKMn7IIiRo1zNmHS5xUn2eMpg.t11jsNvC6cNIgx6xHCHtBw@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal(1709224272, payload.UserData.Id);

            #endregion
        }

        [Fact]
        public void TestExtensionsWithManual()
        {
            #region Arrange

            _services.UseFlipGiveRewards("ABCDEFGH", "sk_abcdefghijklmnop");
            var provider = _services.BuildServiceProvider();

            #endregion

            #region Act

            var flipGiveRewardsService = provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTUDgXxMAaOzBHEmlOYFZUuYxNwn6EwzDUbeqP_d7xdRr2tP0fkSoTJusQXaZtfl-6tocMvAGXUxrLIIAAQbSyRGszkCNBbOZSetElTe6FKQwLudRW_VrV4HGlZoNnuCb6CD7xR2kATiTAGPNNcqpe4hDWeCNUAfDHJ3ujsCwuXDbFiD4hinb6nEOiSgIjAaxsZfyQ6qVbIxBrbVRi_6fGMqrKrEQBNckv-Ps-MavLiURe6gX2JQQvC-1wIqdH42EdkW9G7eQWv_hz99GcYf-lX9UUczGK3CpHG1xLwYqKwdswnxdWzwPDRApCabAEcPhHODH2EAo59UGC4g26Gh9RbZdNdkfrvTJfZ9WIiURrgi01OSjG2CkxEbtnMYTkPhfmcMmKMn7IIiRo1zNmHS5xUn2eMpg.t11jsNvC6cNIgx6xHCHtBw@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal(1709224272, payload.UserData.Id);

            #endregion
        }
    }
}