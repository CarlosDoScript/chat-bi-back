using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat.Bi.API.Filters;

public class FluentValidationMensagensFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var messages = context.ModelState
                   .SelectMany(ms => ms.Value.Errors)
                   .Select(e => e.ErrorMessage)
                   .ToList();

            context.Result = new BadRequestObjectResult(Resultado.Falhar(messages));
        }
    }
}