using WhatsAppNotificationApi.Models;

namespace WhatsAppNotificationApi.services.Interfaces;

public interface IRabbitMqService
{
    Task<ApiResponse<Notification>> QueueNotificationAsync(Notification notification);
}