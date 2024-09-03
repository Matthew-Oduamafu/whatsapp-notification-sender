using WhatsAppNotificationApi.Models;

namespace WhatsAppNotificationWorker.services.Interfaces;

public interface IWhatsAppService
{
    Task<bool> SendWhatsAppMessageAsync(WhatsAppMessage message);
}