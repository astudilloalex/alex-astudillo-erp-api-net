using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/person-document-type")]
[ApiController]
[Authorize]
public class PersonDocumentTypeController : CommonController
{
    private readonly IPersonDocumentTypeService _service;

    public PersonDocumentTypeController(IPersonDocumentTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery(Name = "only_active")] bool? onlyActive)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll(onlyActive)));
    }
}
