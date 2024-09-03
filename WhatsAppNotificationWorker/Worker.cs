using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WhatsAppNotificationApi.Models;
using WhatsAppNotificationWorker.Options;
using WhatsAppNotificationWorker.services.Interfaces;

namespace WhatsAppNotificationWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IOptionsMonitor<RabbitMqConfig> rabbitMqConfigOpt, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _rabbitMqConfig = rabbitMqConfigOpt.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = _rabbitMqConfig.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _rabbitMqConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonConvert.DeserializeObject<WhatsAppMessage>(Encoding.UTF8.GetString(body));
            _logger.LogInformation("Received message: {Message}", message);

            using var scope = _serviceProvider.CreateScope();
            var whatsAppService = scope.ServiceProvider.GetRequiredService<IWhatsAppService>();
            await whatsAppService.SendWhatsAppMessageAsync(message!);
        };

        channel.BasicConsume(queue: _rabbitMqConfig.QueueName, autoAck: true, consumer: consumer);
        await Task.CompletedTask;
    }
}