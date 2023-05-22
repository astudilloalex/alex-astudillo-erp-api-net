using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
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

    [HttpPost]
    [Route("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
    {
        return Ok(ResponseHandler.Ok(await _service.SignIn(signInDTO.Username, signInDTO.Password)));
    }
}
