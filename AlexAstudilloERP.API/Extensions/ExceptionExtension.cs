using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Middlewares;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AlexAstudilloERP.API.Extensions;

public static class ExceptionExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder builder, ILoggerManager logger)
    {
        builder.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    Dictionary<string, object> exceptionProperties = ResponseHandler.Error(context.Response.StatusCode, "internal-server-error");
                    await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionProperties));
                }
            });
        });
    }

    public static void ConfigureExceptionMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
    }
}
