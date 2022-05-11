using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Common;

namespace ScrumPoker.Infrastructure.Middlewares;

public class FooBar
{
    public static IActionResult Process(ActionContext context)
    {
        var errorInModelState =(context.ModelState
            .Where(x=>x.Value.Errors.Count > 0)
            .ToDictionary(kvp=>kvp.Key, kvp =>kvp.Value.Errors
                .Select(x=>x.ErrorMessage))).ToArray();
        
        var errors = errorInModelState.Select(e => new ScrumPokerError
        {
            Field = e.Key,
            Messages = e.Value.ToList()
        });
        
        var errorResponse = new ScrumPokerErrorResponse
        {
            Errors = errors.ToList()
        };
        
        return new BadRequestObjectResult(errorResponse);
    }
}