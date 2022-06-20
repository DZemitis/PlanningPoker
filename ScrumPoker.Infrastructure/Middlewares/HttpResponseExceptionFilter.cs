using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScrumPoker.Common;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Infrastructure.Middlewares;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not ScrumPokerException httpResponseException) return;
        if (httpResponseException.Message != null)
        {
            var statusCode = context.Exception switch
            {
                ConflictException => 409,
                NotFoundException => 404,
                ForbiddenException => 403,
                _ => throw new ArgumentOutOfRangeException
                {
                    HelpLink = null,
                    HResult = 0,
                    Source = null
                }
            };

            var errorResponse = new ScrumPokerError
            {
                Field = context.Exception.GetType().ToString(),
                Messages = new List<string> {httpResponseException.Message}
            };

            var response = new ScrumPokerErrorResponse
            {
                Errors = new List<ScrumPokerError>
                {
                    errorResponse
                }
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }

        context.ExceptionHandled = true;
    }

    public int Order => int.MaxValue - 10;
}