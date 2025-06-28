namespace Comanda.Shared.Errors;

public static class OwnerErrors
{
    public static Error NotFound => new Error(
        Code: "#COMANDA-ERR-OWN-404",
        Description: "Owner not found, this owner does not exist"
    );
}
