using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/company")]
[ApiController]
[Authorize]
public class CompanyController : CommonController
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] Company company)
    {
        return Ok(ResponseHandler.Ok(await _service.AddAsync(company, UserCode)));
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All([FromQuery] int page, [FromQuery] int size)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAllowed(PageRequest.Of(page - 1, size), UserCode)));
    }

    [HttpGet]
    [Route("get/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCode(code, UserCode)));
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update([FromBody] Company company, int id)
    {
        company.Id = id;
        return Ok(ResponseHandler.Ok(await _service.Update(company, UserCode)));
    }
}
