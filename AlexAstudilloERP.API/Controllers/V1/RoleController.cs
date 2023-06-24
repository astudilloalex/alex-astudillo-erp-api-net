﻿using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Mappers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
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

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> All([FromBody] RoleRequestDTO roleRequestDTO)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(DTOToEntity.RoleRequestDTOToRole(roleRequestDTO), Token)));
    }
}
