using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Text;

namespace TT.Log;

public class RabbitOptions 
{
    public string Host { get; set; }
}

public class RabbitMQListner : BackgroundService
{
    private readonly string _host;

    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly IAsyncBasicConsumer _consumer;

    private readonly string _queueName;

    public RabbitMQListner(IOptions<RabbitOptions> options)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.ExchangeDeclareAsync("logs", ExchangeType.Fanout).Wait();

        var queue = _channel.QueueDeclareAsync().GetAwaiter().GetResult();
        _queueName = queue.QueueName;
        _channel.QueueBindAsync(_queueName, "logs", String.Empty);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += Consumer_ReceivedAsync;
        _consumer = consumer;
    }

    private Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs message)
    {
        var msg = message.Body.ToArray();
        var text = Encoding.UTF8.GetString(msg);

        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _channel.BasicConsumeAsync(_queueName, true, _consumer);
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();

        base.Dispose();
    }
}
