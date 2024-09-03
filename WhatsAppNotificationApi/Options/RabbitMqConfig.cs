using System.ComponentModel.DataAnnotations;

namespace WhatsAppNotificationApi.Options;

#pragma warning disable CS8618 

public sealed class RabbitMqConfig
{
    [Required(AllowEmptyStrings = false)]
    public string Host { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string QueueName { get; set; }
}