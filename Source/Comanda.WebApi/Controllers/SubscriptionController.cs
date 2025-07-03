namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/subscriptions")]
public sealed class SubscriptionController(IMediator mediator) : ControllerBase
{
    [HttpPost("subscribe")]
    [Authorize(Roles = Permissions.SubscribePlan)]
    public async Task<IActionResult> SubscribePlanAsync()
    {
        var request = new SubscribePlanRequest();
        var result = await mediator.Send(request);

        return Ok(result.Data);
    }
}