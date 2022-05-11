using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Common;

namespace ScrumPoker.Infrastructure.Middlewares;

public class FooBar
{
    public static IActionResult Process(ActionContext context)
    {
        var response = new ScrumPokerErrorResponse
        {
            Errors = new List<ScrumPokerError>()
        };
        var errorInModelState =(context.ModelState
            .Where(x=>x.Value.Errors.Count > 0)
            .ToDictionary(kvp=>kvp.Key, kvp =>kvp.Value.Errors
                .Select(x=>x.ErrorMessage))).ToArray();

        var errorResponse = new ScrumPokerError()
        {
            Messages = new List<string>()
        };
        
        foreach (var error in errorInModelState)
        {
            errorResponse.Field = error.Key;
            foreach (var subError in error.Value)
            {
                errorResponse.Messages.Add(subError);
            }
        }
        response.Errors.Add(errorResponse);
        
        return new BadRequestObjectResult(response);
    }
}