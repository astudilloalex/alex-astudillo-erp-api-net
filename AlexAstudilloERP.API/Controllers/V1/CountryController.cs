using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/country")]
[ApiController]
[Authorize]
public class CountryController : CommonController
{
    private readonly ICountryService _service;

    public CountryController(ICountryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All([FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll(active)));
    }
}
