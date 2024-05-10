using FlipGiveSDK_dotnet;
using FlipGiveSDK_dotnet.Exceptions;
using Jose;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test_FlipGiveSDK_dotnet.Methods
{
    public class SDKReadTokenTest
    {
        private readonly ServiceProvider _provider;

        public SDKReadTokenTest()
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

            #endregion

            #region Act

            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTUDgXxMAaOzBHEmlOYFZUuYxNwn6EwzDUbeqP_d7xdRr2tP0fkSoTJusQXaZtfl-6tocMvAGXUxrLIIAAQbSyRGszkCNBbOZSetElTe6FKQwLudRW_VrV4HGlZoNnuCb6CD7xR2kATiTAGPNNcqpe4hDWeCNUAfDHJ3ujsCwuXDbFiD4hinb6nEOiSgIjAaxsZfyQ6qVbIxBrbVRi_6fGMqrKrEQBNckv-Ps-MavLiURe6gX2JQQvC-1wIqdH42EdkW9G7eQWv_hz99GcYf-lX9UUczGK3CpHG1xLwYqKwdswnxdWzwPDRApCabAEcPhHODH2EAo59UGC4g26Gh9RbZdNdkfrvTJfZ9WIiURrgi01OSjG2CkxEbtnMYTkPhfmcMmKMn7IIiRo1zNmHS5xUn2eMpg.t11jsNvC6cNIgx6xHCHtBw@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal(1709224272, payload.UserData.Id);

            #endregion
        }

        [Fact]
        public void TestNullReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var payload = flipGiveRewardsService.ReadToken(null);
            }
            catch (Exception invalidTokenException)
            {
                ex = invalidTokenException;
            }

            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is InvalidTokenException);
            Assert.Equal("Null Token", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNotSplittedReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var payload = flipGiveRewardsService.ReadToken("abc");
            }
            catch (Exception invalidTokenException)
            {
                ex = invalidTokenException;
            }

            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is InvalidTokenException);
            Assert.Equal("Token not formed with 2 keys token@cloudShopId", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNotCloudShopIdReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var payload = flipGiveRewardsService.ReadToken("abc@");
            }
            catch (Exception invalidTokenException)
            {
                ex = invalidTokenException;
            }

            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is InvalidTokenException);
            Assert.Equal("Token not formed with 2 keys token@cloudShopId", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNotTokenReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var payload = flipGiveRewardsService.ReadToken("@abc");
            }
            catch (Exception invalidTokenException)
            {
                ex = invalidTokenException;
            }

            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is InvalidTokenException);
            Assert.Equal("Token not formed with 2 keys token@cloudShopId", ex.Message);

            #endregion
        }

        [Fact]
        public void TestInvalidTokenReadToken()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var payload = flipGiveRewardsService.ReadToken("invalid@ABCDEFGH");
            }
            catch (Exception invalidTokenException)
            {
                ex = invalidTokenException;
            }

            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is JoseException);

            #endregion
        }
    }
}