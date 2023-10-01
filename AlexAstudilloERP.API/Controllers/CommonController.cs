using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers;

public class CommonController : ControllerBase
{
    /// <summary>
    /// Returns the current JWT Bearer for each request.
    /// </summary>
    protected string Token
    {
        get
        {
            return Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }
    }

    protected string UserCode
    {
        get
        {
            return Request.Headers["X-User-Code"].ToString();
        }
    }

    protected string CompanyCode
    {
        get
        {
            return Request.Headers["X-Company-Code"].ToString();
        }
    }
}
