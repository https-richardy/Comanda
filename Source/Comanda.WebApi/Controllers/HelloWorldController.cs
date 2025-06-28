using Comanda.Shared.Configuration;

namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/hello-world")]
public sealed class HelloWorldController(ISettings settings) : ControllerBase
{
    [HttpGet]
    public IActionResult GetHelloWorld()
    {
        return Ok(settings);
    }
}