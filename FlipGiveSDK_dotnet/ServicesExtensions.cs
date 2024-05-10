using FlipGiveSDK_dotnet.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace FlipGiveSDK_dotnet
{
    /// <summary>
    /// Extnsion methods to inject FlipGiveRewardsService in application 
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Inject FlipGiveRewardsService as singleton
        /// </summary>
        /// <param name="services">Extension of IServiceCollection</param>
        /// <param name="configureOptions">Binding of a FlipGiveRewardsOptions section</param>
        /// <returns></returns>
        public static IServiceCollection UseFlipGiveRewards(this IServiceCollection services, Action<FlipGiveRewardsOptions> configureOptions)
        {
            services.Configure<FlipGiveRewardsOptions>(configureOptions);
            services.AddSingleton<FlipGiveRewardsService>();

            return services;
        }

        /// <summary>
        /// Inject FlipGiveRewardsService as singleton using app IConfiguration
        /// </summary>
        /// <param name="services">Extension of IServiceCollection</param>
        /// <param name="configuration">The application configuration, needs to have the FlipGiveRewardsOptions already inside</param>
        /// <returns></returns>
        public static IServiceCollection UseFlipGiveRewards(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FlipGiveRewardsOptions>(configuration.GetSection(nameof(FlipGiveRewardsOptions)));
            services.AddSingleton<FlipGiveRewardsService>();

            return services;
        }

        /// <summary>
        /// Inject FlipGiveRewardsService as singleton using cloudShopId and secret as parameters
        /// </summary>
        /// <param name="services">Extension of IServiceCollection</param>
        /// <param name="cloudShopId">The id provided by FlipGive</param>
        /// <param name="secret">The secret provided by FlipGive</param>
        /// <returns></returns>
        public static IServiceCollection UseFlipGiveRewards(this IServiceCollection services, string cloudShopId, string secret)
        {
            FlipGiveRewardsOptions flipGiveRewardsOptions = new FlipGiveRewardsOptions()
            {
                CloudShopId = cloudShopId,
                Secret = secret
            };
            IOptions<FlipGiveRewardsOptions> options = Microsoft.Extensions.Options.Options.Create(flipGiveRewardsOptions);
            services.AddSingleton<IOptions<FlipGiveRewardsOptions>>(options);
            services.AddSingleton<FlipGiveRewardsService>();

            return services;
        }
    }
}
