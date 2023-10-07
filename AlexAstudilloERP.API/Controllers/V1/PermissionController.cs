using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/permission")]
[ApiController]
public class PermissionController : CommonController
{
    private readonly IPermissionService _service;

    public PermissionController(IPermissionService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all/{companyId}")]
    public async Task<IActionResult> All(int companyId, [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        IPageable pageable = PageRequest.Of(page - 1, size);
        return Ok(ResponseHandler.Ok(await _service.GetAll(pageable, companyId, Token)));
    }
}
