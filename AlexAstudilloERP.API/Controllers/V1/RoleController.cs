using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Mappers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/role")]
[ApiController]
[Authorize]
public class RoleController : CommonController
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all/{companyId}")]
    public async Task<IActionResult> All(int companyId, [FromQuery] int page, [FromQuery] int size, [FromQuery] bool? active = null)
    {
        IPageable pageable = PageRequest.Of(page - 1, size);
        return Ok(ResponseHandler.Ok(await _service.GetAll(
            pageable: pageable,
            companyId: companyId,
            token: Token,
            active: active
        )));
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] RoleRequestDTO roleRequestDTO)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(DTOToEntity.RoleRequestDTOToRole(roleRequestDTO), Token)));
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleRequestDTO roleRequestDTO)
    {
        Role role = DTOToEntity.RoleRequestDTOToRole(roleRequestDTO);
        role.Id = id;
        return Ok(ResponseHandler.Ok(await _service.Update(role, Token)));
    }
}
