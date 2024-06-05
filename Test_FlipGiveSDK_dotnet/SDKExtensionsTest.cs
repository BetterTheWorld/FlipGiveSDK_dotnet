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
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTVPmWBoLaOrHGUy7OcgWVuI8fF_6RE_cRLyBd596y9Zg3NP3fjPuZYKgWGGMsf1kod9debMGHw9oKM1fAXGR9gSnmgiaBL2bQu1EljevCqR_Y_FQWulmB8LG7PkA_dOVt2-4ywyoGD6UOGnzJcr_INFbS6bGDhTODpXrjM2wvHbFA3y7x26XsT4oyn9arg67r8esFP2WaYhKqLVHlevOW5LsLr8WGJJx78i7rIqUNSxdPuQTlc1cqzK5jMTNJe7wdireNPHREfDaxtdcT8GlmjwfD5XecyO7H31yLAQkKxl6lSYOEHwPUl8lAbiWUr69SqKKky0t4tINec5rtzo7d6NCPdBFtfqdNNEWKidWoBD4-Oi0TSOmuBCv0Y0Blf1YktM4PpijZ8TJ80aA1ziO9n72P5VeHyoJevxd.VuoEQKhXl77XP70G0rowPQ@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal("1709224272", payload.UserData.Id);

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
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTVPmWBoLaOrHGUy7OcgWVuI8fF_6RE_cRLyBd596y9Zg3NP3fjPuZYKgWGGMsf1kod9debMGHw9oKM1fAXGR9gSnmgiaBL2bQu1EljevCqR_Y_FQWulmB8LG7PkA_dOVt2-4ywyoGD6UOGnzJcr_INFbS6bGDhTODpXrjM2wvHbFA3y7x26XsT4oyn9arg67r8esFP2WaYhKqLVHlevOW5LsLr8WGJJx78i7rIqUNSxdPuQTlc1cqzK5jMTNJe7wdireNPHREfDaxtdcT8GlmjwfD5XecyO7H31yLAQkKxl6lSYOEHwPUl8lAbiWUr69SqKKky0t4tINec5rtzo7d6NCPdBFtfqdNNEWKidWoBD4-Oi0TSOmuBCv0Y0Blf1YktM4PpijZ8TJ80aA1ziO9n72P5VeHyoJevxd.VuoEQKhXl77XP70G0rowPQ@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal("1709224272", payload.UserData.Id);

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
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTVPmWBoLaOrHGUy7OcgWVuI8fF_6RE_cRLyBd596y9Zg3NP3fjPuZYKgWGGMsf1kod9debMGHw9oKM1fAXGR9gSnmgiaBL2bQu1EljevCqR_Y_FQWulmB8LG7PkA_dOVt2-4ywyoGD6UOGnzJcr_INFbS6bGDhTODpXrjM2wvHbFA3y7x26XsT4oyn9arg67r8esFP2WaYhKqLVHlevOW5LsLr8WGJJx78i7rIqUNSxdPuQTlc1cqzK5jMTNJe7wdireNPHREfDaxtdcT8GlmjwfD5XecyO7H31yLAQkKxl6lSYOEHwPUl8lAbiWUr69SqKKky0t4tINec5rtzo7d6NCPdBFtfqdNNEWKidWoBD4-Oi0TSOmuBCv0Y0Blf1YktM4PpijZ8TJ80aA1ziO9n72P5VeHyoJevxd.VuoEQKhXl77XP70G0rowPQ@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal("1709224272", payload.UserData.Id);

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
            var payload = flipGiveRewardsService.ReadToken("eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIiwiaXYiOiJwazBQRWdLOEZPb3V6SkdMIn0..pk0PEgK8FOouzJGL.0huaJ53q5Lqbs6OX5_THSGQiTVPmWBoLaOrHGUy7OcgWVuI8fF_6RE_cRLyBd596y9Zg3NP3fjPuZYKgWGGMsf1kod9debMGHw9oKM1fAXGR9gSnmgiaBL2bQu1EljevCqR_Y_FQWulmB8LG7PkA_dOVt2-4ywyoGD6UOGnzJcr_INFbS6bGDhTODpXrjM2wvHbFA3y7x26XsT4oyn9arg67r8esFP2WaYhKqLVHlevOW5LsLr8WGJJx78i7rIqUNSxdPuQTlc1cqzK5jMTNJe7wdireNPHREfDaxtdcT8GlmjwfD5XecyO7H31yLAQkKxl6lSYOEHwPUl8lAbiWUr69SqKKky0t4tINec5rtzo7d6NCPdBFtfqdNNEWKidWoBD4-Oi0TSOmuBCv0Y0Blf1YktM4PpijZ8TJ80aA1ziO9n72P5VeHyoJevxd.VuoEQKhXl77XP70G0rowPQ@ABCDEFGH");

            #endregion

            #region Assert

            Assert.NotNull(payload);
            Assert.Equal("1709224272", payload.UserData.Id);

            #endregion
        }
    }
}