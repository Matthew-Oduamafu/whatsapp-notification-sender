using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using WhatsAppNotificationApi.Models;
using WhatsAppNotificationApi.Options;
using WhatsAppNotificationApi.services.Interfaces;

namespace WhatsAppNotificationApi.services.Providers;

public sealed class RabbitMqService : IRabbitMqService
{
    private readonly ILogger<RabbitMqService> _logger;
    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly TwilioConfig _twilioConfig;

    public RabbitMqService(ILogger<RabbitMqService> logger, IOptionsMonitor<RabbitMqConfig> rabbitMqConfigOpt, IOptionsMonitor<TwilioConfig> twilioConfigOpt)
    {
        _logger = logger;
        _twilioConfig = twilioConfigOpt.CurrentValue;
        _rabbitMqConfig = rabbitMqConfigOpt.CurrentValue;
    }

    /// <summary>
    /// Queues a notification for sending via RabbitMQ.
    /// </summary>
    /// <param name="notification">The notification to be queued.</param>
    /// <returns>An ApiResponse containing the queued notification data, success message, and status code.</returns>
    public async Task<ApiResponse<Notification>> QueueNotificationAsync(Notification notification)
    {
        _logger.LogDebug("Queueing notification: {Message} for {ToPhoneNumber}", JsonSerializer.Serialize(notification),
            notification.ToPhoneNumber);

        var factory = new ConnectionFactory() { HostName = _rabbitMqConfig.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _rabbitMqConfig.QueueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        WhatsAppMessage message = new(notification.ToPhoneNumber, notification.Message, _twilioConfig.PhoneNumber);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        channel.BasicPublish(exchange: "",
            routingKey: _rabbitMqConfig.QueueName,
            basicProperties: null,
            body: body);

        _logger.LogInformation("Notification queued: {Message} for {ToPhoneNumber}",
            JsonSerializer.Serialize(notification), notification.ToPhoneNumber);

        await Task.CompletedTask;
        
        return new ApiResponse<Notification>
        {
            Data = notification,
            Message = "Notification queued successfully",
            Code = StatusCodes.Status200OK
        };
    }
}