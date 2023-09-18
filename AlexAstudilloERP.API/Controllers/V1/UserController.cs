using AlexAstudilloERP.API.Attributes;
using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByToken(Token)));
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
