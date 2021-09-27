using System;
using Web.Client.Abstract;
using Web.Client.Concrete;
using Web.Core.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.API.Configuration.DependencyInjectionExtension
{
    public static class HttpClientExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClientSettings = configuration.GetSection("HttpClientSettings").Get<HttpClientSettings>();

            services.AddHttpClient<IProductClient, ProductClient>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(httpClientSettings.Product);
                });

            services.AddHttpClient<ICampaignClient, CampaignClient>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(httpClientSettings.Campaign);
                });

            services.AddHttpClient<IOrderClient, OrderClient>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(httpClientSettings.Order);
                });

            services.AddHttpClient<IBasketClient, BasketClient>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(httpClientSettings.Basket);
                });
        }
    }
}
