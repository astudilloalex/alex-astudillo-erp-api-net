using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/political-division-type")]
[ApiController]
[Authorize]
public class PoliticalDivisionTypeController : CommonController
{
    private readonly IPoliticalDivisionTypeService _service;

    public PoliticalDivisionTypeController(IPoliticalDivisionTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll(active)));
    }
}
