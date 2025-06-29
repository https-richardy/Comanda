namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/owner")]
public sealed class OwnerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOwnerDetailsAsync(GetOwnerDetails request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status400BadRequest, result.Error);
        }

        return Ok(result.Data);
    }
}