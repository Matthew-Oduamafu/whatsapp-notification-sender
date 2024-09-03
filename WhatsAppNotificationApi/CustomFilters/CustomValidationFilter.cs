using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WhatsAppNotificationApi.Models;

namespace WhatsAppNotificationApi.CustomFilters;

public class CustomValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errors = context.ModelState
            .Where(ms => ms.Value is { Errors.Count: > 0 })
            .Select(ms => new Error
            {
                Field = ms.Key,
                Message = ms.Value?.Errors?.First()?.ErrorMessage!
            })
            .ToList();

        var response = new ApiResponse<object>
        {
            Message = "Validation failed",
            Code = StatusCodes.Status400BadRequest,
            Errors = errors
        };

        context.Result = new BadRequestObjectResult(response);
    }
}