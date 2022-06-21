using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Infrastructure.Middlewares;

public class ValidationFilter
{
    public static IActionResult Process(ActionContext context)
    {
        var errorInModelState = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors
                .Select(x => x.ErrorMessage)).ToArray();

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