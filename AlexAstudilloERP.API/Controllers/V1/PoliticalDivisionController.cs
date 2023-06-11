using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/political-division")]
[ApiController]
[Authorize]
public class PoliticalDivisionController : CommonController
{
    private readonly IPoliticalDivisionService _service;

    public PoliticalDivisionController(IPoliticalDivisionService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("get-by-parent-id/{parentId}")]
    public async Task<IActionResult> GetAll(int parentId, [FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByParentId(parentId, active)));
    }

    [HttpGet]
    [Route("get-by-type-id/{typeId}")]
    public async Task<IActionResult> GetByTypeId(short typeId, [FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByTypeId(typeId, active)));
    }

    [HttpGet]
    [Route("get-by-country-and-type/{countryId}")]
    public async Task<IActionResult> GetByCountryAndTypeId(short countryId, [FromQuery(Name = "type_id")] short typeId, [FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByTypeIdAndCountryId(countryId, typeId, active)));
    }
}
