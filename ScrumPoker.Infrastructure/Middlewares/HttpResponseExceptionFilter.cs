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
            if (httpResponseException.Message != null)
            {
                var statusCode = context.Exception switch
                {
                    ConflictException => 409,
                    NotFoundException => 404,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                var response = new ScrumPokerErrorResponse
                {
                    Errors = new List<ScrumPokerError>()
                 
                };

                var errorResponse = new ScrumPokerError()
                {
                    Field = context.Exception.GetType().ToString(),
                    Messages = new List<string> {httpResponseException.Message}
                };


                response.Errors.Add(errorResponse);
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    Value = response,
                    StatusCode = statusCode,
                };
            }

            context.ExceptionHandled = true;
        }
    }
}