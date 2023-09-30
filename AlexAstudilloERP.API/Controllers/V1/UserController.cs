using AlexAstudilloERP.API.Attributes;
using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/user")]
[ApiController]
public class UserController : CommonController
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("exchange-refresh-token")]
    [SkipTokenValidation]
    public async Task<IActionResult> ExchangeRefreshToken([FromBody] ExchangeRefreshTokenRequest request)
    {
        return Ok(ResponseHandler.Ok(await _service.ExchangeRefreshTokenForIdToken(request.RefreshToken)));
    }

    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCodeAsync(UserCode)));
    }

    [HttpPost]
    [Route("sign-in")]
    [SkipTokenValidation]
    public async Task<IActionResult> SignIn([FromBody] SignInDTORequest request)
    {
        return Ok(ResponseHandler.Ok(await _service.SignIn(request.Email, request.Password)));
    }

    [HttpPost]
    [Route("sign-up")]
    [SkipTokenValidation]
    public async Task<IActionResult> SignUp([FromBody] SignInDTORequest request)
    {
        return Ok(ResponseHandler.Ok(await _service.SignUp(request.Email, request.Password)));
    }
}
