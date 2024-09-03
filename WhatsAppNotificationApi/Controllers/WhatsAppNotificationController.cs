using Microsoft.AspNetCore.Mvc;
using WhatsAppNotificationApi.CustomFilters;
using WhatsAppNotificationApi.Models;
using WhatsAppNotificationApi.services.Interfaces;

namespace WhatsAppNotificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(CustomValidationFilter))]
public class WhatsAppNotificationController : ControllerBase
{
    private readonly IRabbitMqService _rabbitMqService;

    public WhatsAppNotificationController(IRabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Post([FromBody] Notification notification)
    {
        var response =  await _rabbitMqService.QueueNotificationAsync(notification);
        return StatusCode(response.Code, response);
    }
}