using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Common;

namespace ScrumPoker.Infrastructure.Middlewares;

public class FooBar
{
    public static IActionResult Process(ActionContext context)
    {
        var response = new ScrumPokerErrorResponse
        {
            Messages = new List<string>()
        };
        var errorInModelState =(context.ModelState
            .Where(x=>x.Value.Errors.Count > 0)
            .ToDictionary(kvp=>kvp.Key, kvp =>kvp.Value.Errors
                .Select(x=>x.ErrorMessage))).ToArray();

        foreach (var error in errorInModelState)
        {
            foreach (var subError in error.Value)
            {
                response.Messages.Add(subError);
            }
        }
        
        return new BadRequestObjectResult(response);
    }
}