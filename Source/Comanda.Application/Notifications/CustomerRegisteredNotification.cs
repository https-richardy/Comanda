namespace Comanda.Application.Notifications;

public record CustomerRegisteredNotification(EnrollmentCredentials Credentials) : INotification;