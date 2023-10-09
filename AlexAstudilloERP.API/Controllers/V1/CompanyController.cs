using AlexAstudilloERP.API.DTOs;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using AutoMapper;
using EFCommonCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/company")]
[ApiController]
public class CompanyController : CommonController
{
    private readonly ICompanyService _service;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CompanyDTO company)
    {
        return Ok(ResponseHandler.Ok(await _service.AddAsync(_mapper.Map<Company>(company), UserCode)));
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All([FromQuery] int? page, [FromQuery] int? size)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAllowed(PageRequest.Of((page ?? 1) - 1, size ?? 10), UserCode)));
    }

    [HttpGet]
    [Route("get/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCode(code, UserCode)));
    }

    [HttpPut]
    [Route("update/{code}")]
    public async Task<IActionResult> Update([FromBody] CompanyDTO company, string code)
    {
        company.Code = code;
        return Ok(ResponseHandler.Ok(await _service.Update(_mapper.Map<Company>(company), UserCode)));
    }
}
