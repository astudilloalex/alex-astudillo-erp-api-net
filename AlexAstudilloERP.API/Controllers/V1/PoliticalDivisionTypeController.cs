using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/political-division-type")]
[ApiController]
public class PoliticalDivisionTypeController(IPoliticalDivisionTypeService _service) : CommonController
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll(active)));
    }
}
