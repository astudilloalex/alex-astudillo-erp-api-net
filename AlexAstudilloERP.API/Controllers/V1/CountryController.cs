using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace AlexAstudilloERP.API.Controllers.V1;

[Route("api/v1/country")]
[ApiController]
public class CountryController(ICountryService _service) : CommonController
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All([FromQuery] bool? active)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAll(active)));
    }
}
