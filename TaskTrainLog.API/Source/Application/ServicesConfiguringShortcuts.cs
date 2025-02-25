using System.Text;
using TT.Core;

namespace TT.Log;

public static class ServicesConfiguringShortcuts
{
    public static IServiceCollection AddLogListner(this IServiceCollection services) 
    {
        services.AddHostedService<RabbitMQSubscriber>();
        services.Configure<RabbitMQSubscriberOptions>(options => 
        {
            options.Host = "localhost";
            options.ExchangeName = "Log";
            options.ExchangeType = ExchangeTypeEnum.Fanout;
            options.OnMessageRecived = (bytes) => 
            {
                var message = Encoding.UTF8.GetString(bytes);
            };
        });
        return services;
    }
}
