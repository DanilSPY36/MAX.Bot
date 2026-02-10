using MAX.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace MAX.Bot.Services
{
    public static class MaxBotServiceCollectionExtensions
    {
        /// <summary>
        /// регистрирует MaxBotClient в контейнере зависимостей
        /// необходимо вызвать в методе ConfigureServices класса Program
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaxBot(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<BotConfiguration>(config.GetSection("BotConfiguration"));

            /*var section = config.GetSection("BotConfiguration");
            foreach (var item in section.GetChildren())
            {
                Console.WriteLine($"{item.Key} = {item.Value}");
            }*/

            services.AddHttpClient<IMaxBotClient, MaxBotClient>((sp, client) =>
            {
                var botConfig = sp
                    .GetRequiredService<IOptions<BotConfiguration>>()
                    .Value;

                client.BaseAddress = new Uri("https://platform-api.max.ru/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(botConfig.BotToken);
            });

            return services;
        }
    }
}
