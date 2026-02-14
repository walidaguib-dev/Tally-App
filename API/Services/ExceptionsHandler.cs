using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Services
{
    public static class ExceptionsHandler
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception is ValidationException validationException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var errors = validationException.Errors
                            .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });
                        await context.Response.WriteAsJsonAsync(new { errors });
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
                    }
                });
            });

            return builder;
        }



    }
}
