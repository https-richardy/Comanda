namespace Comanda.Application.Notifications;

public record EstablishmentOwnerRegisteredNotification(EnrollmentCredentials Credentials) : INotification;