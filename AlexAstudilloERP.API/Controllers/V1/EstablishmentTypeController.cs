using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/establishment-type")]
[ApiController]
public class EstablishmentTypeController : CommonController
{
    private readonly IEstablishmentTypeService _service;

    public EstablishmentTypeController(IEstablishmentTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll()));
    }
}
