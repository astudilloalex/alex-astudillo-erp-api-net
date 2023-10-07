using AlexAstudilloERP.API.DTOs;
using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Mappers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
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
    [Route("get/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCodeAsync(code, UserCode, CompanyCode)));
    }

    [HttpGet]
    [Route("get-by-id-card")]
    public async Task<IActionResult> All([FromQuery(Name = "id_card")] string idCard)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdCard(idCard, UserCode, CompanyCode)));
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CustomerDTO request)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(DTOToEntity.CustomerDTOToCustomer(request), UserCode, CompanyCode)));
    }

    [HttpPut]
    [Route("update/{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] CustomerDTO request)
    {
        request.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(DTOToEntity.CustomerDTOToCustomer(request), UserCode, CompanyCode)));
    }

    [HttpPatch]
    [Route("change-state/{code}")]
    public async Task<IActionResult> ChangeState(string code, [FromBody] ChangeStateDTORequest request)
    {
        return Ok(ResponseHandler.Ok(await _service.ChangeStateAsync(new()
        {
            Code = code,
            Active = request.Active,
        }, UserCode, CompanyCode)));
    }
}
