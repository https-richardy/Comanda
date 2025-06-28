namespace Comanda.Shared.Errors;

public static class EstablishmentErrors
{
    public static Error NotFound => new Error(
        Code: "#COMANDA-ERR-EST-404",
        Description: "Establishment not found, this establishment does not exist"
    );

    public static Error OwnerMismatch => new Error(
        Code: "#COMANDA-ERR-EST-606",
        Description: "This establishment does not belong to the current user"
    );

    public static Error InactiveSubscription => new Error(
        Code: "#COMANDA-ERR-EST-607",
        Description: "This establishment requires an active subscription to operate"
    );

    public static Error OutsideOperatingHours => new Error(
        Code: "#COMANDA-ERR-EST-608",
        Description: "This establishment is currently outside of its operating hours and cannot accept new orders"
    );
}