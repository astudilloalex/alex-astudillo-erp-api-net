using AlexAstudilloERP.Domain.Interfaces.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace AlexAstudilloERP.API.Handlers;

public class AuthorizeHandler : JwtBearerEvents
{
    public AuthorizeHandler()
    {
        OnForbidden = async context =>
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                Dictionary<string, object> response = ResponseHandler.Forbidden();
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        };
        //OnAuthenticationFailed = async context =>
        //{
        //    if (!context.Response.HasStarted)
        //    {
        //        context.Response.StatusCode = 401;
        //        context.Response.ContentType = "application/json";
        //        Dictionary<string, object> response = ResponseHandler.Unauthorized("unauthorized");
        //        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        //    }
        //};
        OnChallenge = async context =>
        {
            if (!context.Response.HasStarted)
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                Dictionary<string, object> response = ResponseHandler.Unauthorized("unauthorized");
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        };
        OnTokenValidated = async context =>
        {
            if (!context.Response.HasStarted)
            {
                IJwtBlacklistService? blacklistService = context.HttpContext.RequestServices.GetService<IJwtBlacklistService>();
                string? token = (context.SecurityToken as JwtSecurityToken)?.RawData;
                bool blaclistToken = blacklistService != null && await blacklistService.ExistsByToken(token ?? "");
                if (blaclistToken)
                {
                    context.Fail("invalid-token");
                }
            }
        };
    }
}
