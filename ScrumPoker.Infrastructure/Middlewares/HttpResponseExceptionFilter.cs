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
            context.Result = new ObjectResult(httpResponseException.Value)
            {
                StatusCode = httpResponseException.StatusCode,
                Value = httpResponseException.Value
            };

            context.ExceptionHandled = true;
        }
    }
}