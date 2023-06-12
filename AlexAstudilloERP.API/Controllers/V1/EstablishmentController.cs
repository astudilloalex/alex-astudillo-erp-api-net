using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/establishment")]
[ApiController]
[Authorize]
public class EstablishmentController : CommonController
{
    private readonly IEstablishmentService _service;

    public EstablishmentController(IEstablishmentService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] Establishment establishment)
    {
        return Ok(ResponseHandler.Ok(await _service.Add(establishment, Token)));
    }
}
