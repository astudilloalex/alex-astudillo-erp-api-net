using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/menu")]
[ApiController]
public class MenuController(IMenuService _service) : CommonController
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByUserCodeAndCompanyCodeAsync(UserCode, CompanyCode)));
    }

    [HttpGet]
    [Route("all/parents")]
    public async Task<IActionResult> GetParents()
    {
        return Ok(ResponseHandler.Ok(await _service.GetParentsAsync(UserCode, CompanyCode)));
    }
}
