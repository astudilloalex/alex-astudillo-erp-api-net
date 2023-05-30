using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/user")]
[ApiController]
[Authorize]
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
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
    {
        return Ok(ResponseHandler.Ok(await _service.SignIn(signInDTO.Username, signInDTO.Password)));
    }

    [HttpPost]
    [Route("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] User user)
    {
        return Ok(ResponseHandler.Ok(await _service.SignUp(user)));
    }
}
