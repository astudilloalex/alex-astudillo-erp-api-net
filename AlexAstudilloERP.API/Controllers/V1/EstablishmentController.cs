using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/establishment")]
[ApiController]
[Authorize]
public class EstablishmentController(IEstablishmentService _service) : CommonController
{
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] Establishment establishment)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(establishment, Token)));
    }

    [HttpGet]
    [Route("all/{companyId}")]
    public async Task<IActionResult> All(int companyId, [FromQuery] int page, [FromQuery] int size)
    {
        return Ok(ResponseHandler.Ok(await _service.FindByCompanyId(PageRequest.Of(page - 1, size), companyId, Token)));
    }

    [HttpGet]
    [Route("get/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCode(code, Token)));
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update([FromBody] Establishment establishment, int id)
    {
        establishment.Id = id;
        return Ok(ResponseHandler.Ok(await _service.Update(establishment, Token)));
    }

    [HttpPatch]
    [Route("set-main/{id}")]
    public async Task<IActionResult> SetMain(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.SetMain(id, Token)));
    }
}
