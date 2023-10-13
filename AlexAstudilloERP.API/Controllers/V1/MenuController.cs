using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/menu")]
[ApiController]
public class MenuController : CommonController
{
    private readonly IMenuService _service;

    public MenuController(IMenuService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByUserCodeAndCompanyCodeAsync(UserCode, CompanyCode)));
    }

    [HttpGet]
    [Route("parents")]
    public async Task<IActionResult> GetParents()
    {
        return Ok(ResponseHandler.Ok(await _service.GetParentsAsync(UserCode, CompanyCode)));
    }
}
