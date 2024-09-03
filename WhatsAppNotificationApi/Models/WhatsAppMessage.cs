namespace WhatsAppNotificationApi.Models;

public class WhatsAppMessage
{
    public string ToPhoneNumber { get; set; }
    public string Message { get; set; }
    public string FromPhoneNumber { get; set; }
    
    public WhatsAppMessage(string toPhoneNumber, string message, string fromPhoneNumber)
    {
        ToPhoneNumber = toPhoneNumber;
        Message = message;
        FromPhoneNumber = fromPhoneNumber;
    }
}