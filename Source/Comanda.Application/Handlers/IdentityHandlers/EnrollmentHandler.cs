namespace Comanda.Application.Handlers;

public sealed class EnrollmentHandler(IIdentityGateway identityGateway, IMediator mediator) :
    IRequestHandler<EnrollmentCredentials, Result>
{
    public async Task<Result> Handle(EnrollmentCredentials request, CancellationToken cancellationToken)
    {
        var result = await identityGateway.EnrollAsync(request);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        var userId = result.Data;
        var group = request.Type.ToString();

        var assignResult = await identityGateway.AssignUserToGroupAsync(userId, group);
        if (assignResult.IsFailure)
        {
            return Result.Failure(assignResult.Error);
        }

        var notification = new UserEnrolledNotification(request);
        await mediator.Publish(notification, cancellationToken);

        return Result.Success();
    }
}