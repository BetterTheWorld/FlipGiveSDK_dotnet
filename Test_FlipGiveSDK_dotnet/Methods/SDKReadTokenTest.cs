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

            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTVPmWBoLaOrHGUy7OcgWVuI8fF_6RE_cRLyBd596y9Zg3NP3fjPuZYKgWGGMsf1kod9debMGHw9oKM1fAXGR9gSnmgiaBL2bQu1EljevCqR_Y_FQWulmB8LG7PkA_dOVt2-4ywyoGD6UOGnzJcr_INFbS6bGDhTODpXrjM2wvHbFA3y7x26XsT4oyn9arg67r8esFP2WaYhKqLVHlevOW5LsLr8WGJJx78i7rIqUNSxdPuQTlc1cqzK5jMTNJe7wdireNPHREfDaxtdcT8GlmjwfD5XecyO7H31yLAQkKxl6lSYOEHwPUl8lAbiWUr69SqKKky0t4tINec5rtzo7d6NCPdBFtfqdNNEWKidWoBD4-Oi0TSOmuBCv0Y0Blf1YktM4PpijZ8TJ80aA1ziO9n72P5VeHyoJevxd.VuoEQKhXl77XP70G0rowPQ@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal("1709224272", payload.UserData.Id);

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