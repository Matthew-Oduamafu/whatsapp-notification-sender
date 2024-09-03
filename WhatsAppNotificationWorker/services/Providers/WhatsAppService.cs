using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WhatsAppNotificationApi.Models;
using WhatsAppNotificationWorker.Options;
using WhatsAppNotificationWorker.services.Interfaces;

namespace WhatsAppNotificationWorker.services.Providers;

public class WhatsAppService : IWhatsAppService
{
    private readonly ILogger<WhatsAppService> _logger;
    private readonly TwilioConfig _twilioConfig;

    public WhatsAppService(ILogger<WhatsAppService> logger, IOptionsMonitor<TwilioConfig> twilioConfigOpt)
    {
        _logger = logger;
        _twilioConfig = twilioConfigOpt.CurrentValue;

        // connect to Twilio client
        TwilioClient.Init(_twilioConfig.AccountSid, _twilioConfig.AuthToken);
    }

    /// <summary>
    /// Sends a WhatsApp message asynchronously.
    /// </summary>
    /// <param name="message">The WhatsApp message to be sent.</param>
    /// <returns>True if the message was sent successfully; otherwise, false.</returns>
    public async Task<bool> SendWhatsAppMessageAsync(WhatsAppMessage message)
    {
        _logger.LogInformation("Sending message to {ToPhoneNumber}", message.ToPhoneNumber);
        
        var messageOptions = new CreateMessageOptions(
            new PhoneNumber("whatsapp:" + message.ToPhoneNumber))
        {
            From = new PhoneNumber("whatsapp:" + _twilioConfig.PhoneNumber),
            Body = message.Message
        };
        var msg = await MessageResource.CreateAsync(messageOptions);

        _logger.LogInformation("Message sent: {Sid}", msg.Sid);
        return string.IsNullOrEmpty(msg.Sid);
    }
}