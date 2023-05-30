using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Exceptions.BadRequest;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Exceptions.Unauthorized;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace AlexAstudilloERP.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILoggerManager _logger;
    private readonly RequestDelegate _requestDelegate;

    public ExceptionMiddleware(ILoggerManager logger, RequestDelegate requestDelegate)
    {
        _logger = logger;
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                if (context.Response.StatusCode == 404)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(ResponseHandler.NotFound()));
                }
            }
        }
        catch (AccessViolationException e)
        {
            _logger.LogError($"");
            await HandleExceptionAsync(context, e);
        }
        catch (NotImplementedException e)
        {
            _logger.LogWarning($"Not implemented exception: {e}");
            await HandleExceptionAsync(context, e);
        }
        catch (UnauthorizedException e)
        {
            _logger.LogError($"Unauthorized exception: {e}");
            await HandleExceptionAsync(context, e);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError($"Database Update Exception: {e}");
            await HandleExceptionAsync(context, e.GetBaseException());
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Dictionary<string, object> exceptionProperties = exception switch
        {
            ForbiddenException => ForbiddenExceptionData((ForbiddenException)exception),
            PostgresException => PostgresExceptionData((PostgresException)exception),
            UnauthorizedException => UnauthorizedExceptionData((UnauthorizedException)exception),
            BadRequestException => BadRequestExceptionData((BadRequestException)exception),
            ConflictException => ConflictExceptionData((ConflictException)exception),
            _ => ResponseHandler.Error(500, exception.Message),
        };
        ;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exceptionProperties["statusCode"];
        await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionProperties));
    }

    private static Dictionary<string, object> PostgresExceptionData(PostgresException exception)
    {
        return exception.SqlState switch
        {
            "22001" => ResponseHandler.Conflict($"data-is-too-long-or-too-small"),
            "23503" => ResponseHandler.Conflict($"foreign-key-do-not-exist-{exception.ConstraintName}"),
            "23505" => ResponseHandler.Conflict($"duplicate-unique-constraint-{exception.ConstraintName}"),
            _ => ResponseHandler.Error((int)HttpStatusCode.InternalServerError, exception.Message),
        };
    }

    private static Dictionary<string, object> UnauthorizedExceptionData(UnauthorizedException exception)
    {
        return exception switch
        {
            _ => ResponseHandler.Unauthorized(exception.Code),
        };
    }

    private static Dictionary<string, object> ForbiddenExceptionData(ForbiddenException exception)
    {
        return exception switch
        {
            _ => ResponseHandler.Forbidden(exception.Code),
        };
    }

    private static Dictionary<string, object> BadRequestExceptionData(BadRequestException exception)
    {
        return exception switch
        {
            _ => ResponseHandler.BadRequest(exception.Code),
        };
    }

    private static Dictionary<string, object> ConflictExceptionData(ConflictException exception)
    {
        return exception switch
        {
            _ => ResponseHandler.Conflict(exception.Code),
        };
    }
}
