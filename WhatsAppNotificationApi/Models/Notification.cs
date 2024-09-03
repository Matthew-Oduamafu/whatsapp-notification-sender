using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhatsAppNotificationApi.Models;

public class Notification
{
    [Required(AllowEmptyStrings = false)]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string ToPhoneNumber { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Message { get; set; }
}