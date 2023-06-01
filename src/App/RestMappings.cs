using Domain.Web;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace App;

public static class RestMappings
{
    public static async Task<Results<Ok<UploadFileResponse>, NoContent>> Import(
        HttpContext context,
        [FromServices] IMediator mediator)
    {
        if (context.Request.Form.Files.Count == 0)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("No file uploaded");
            return TypedResults.NoContent();
        }

        var response = await mediator.Send(new UploadFileCommand(context.Request.Form.Files[0]));

        return TypedResults.Ok(response);
    }
}
