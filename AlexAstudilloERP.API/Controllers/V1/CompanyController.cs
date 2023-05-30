using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
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
        return Ok(ResponseHandler.Ok(await _service.AddAsync(company, Token)));
    }
}
