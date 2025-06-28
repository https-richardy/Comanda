namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/identity")]
public sealed class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> EnrollAsync(EnrollmentCredentials request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status400BadRequest, result.Error);
        }

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticationCredentials request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, result.Error);
        }

        return StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, result.Error);
        }

        return StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(LogoutRequest request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, result.Error);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}