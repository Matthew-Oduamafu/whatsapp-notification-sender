namespace WhatsAppNotificationApi.Models;

public class ApiResponse<T> where T : class
{
    public string Message { get; set; }
    public int Code { get; set; }
    public T Data { get; set; }
    public List<Error> Errors { get; set; }
}