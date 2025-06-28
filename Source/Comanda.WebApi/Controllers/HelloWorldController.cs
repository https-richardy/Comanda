namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/hello-world")]
public sealed class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHelloWorld()
    {
        return Ok("Hello, World!");
    }
}