using System.ComponentModel.DataAnnotations;

namespace WhatsAppNotificationApi.Options;

#pragma warning disable CS8618

public sealed class TwilioConfig
{
    [Required(AllowEmptyStrings = false)]
    public string AccountSid { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string AuthToken { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string PhoneNumber { get; set; }
}