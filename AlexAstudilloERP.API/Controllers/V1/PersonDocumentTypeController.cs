﻿using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/person-document-type")]
[ApiController]
public class PersonDocumentTypeController(IPersonDocumentTypeService _service) : CommonController
{
    [HttpGet]
    [Route("all/{countryCode}")]
    public async Task<IActionResult> GetAll(string countryCode, [FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCountryCodeAsync(countryCode, active)));
    }
}
