using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScrumPoker.Common;
using ScrumPoker.Common.NotFoundExceptions;

namespace ScrumPoker.Infrastructure.Middlewares;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ScrumPokerException httpResponseException)
        {
            var response = new ScrumPokerErrorResponse
            {
                Messages = new List<string> {httpResponseException.Message}
            };

            context.Result = new ObjectResult(httpResponseException.Value)
            {
                Value = response,
                StatusCode = httpResponseException.StatusCode,
            };

            context.ExceptionHandled = true;
        }
    }
}