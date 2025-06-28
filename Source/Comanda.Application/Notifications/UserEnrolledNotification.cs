namespace Comanda.Application.Notifications;

public sealed record UserEnrolledNotification(EnrollmentCredentials Credentials) : INotification;