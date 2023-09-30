using AlexAstudilloERP.API.DTOs;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Mappers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/customer")]
[ApiController]
public class CustomerController : CommonController
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("get-by-id-card")]
    public async Task<IActionResult> All([FromQuery(Name = "company_id")] int companyId, [FromQuery(Name = "id_card")] string idCard)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdCardAndCompanyId(companyId, idCard, Token)));
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CustomerDTO request)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(DTOToEntity.CustomerDTOToCustomer(request), UserCode)));
    }
}
