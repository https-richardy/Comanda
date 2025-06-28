namespace Comanda.Shared.Errors;

public static class OrderErrors
{
    public static Error OrderCannotBeCancelled => new Error(
        Code: "#COMANDA-ERR-ORD-503",
        Description: "The order is in a state that does not allow cancellation"
    );

    public static Error OrderRequiresAtLeastOneItem => new Error(
        Code: "#COMANDA-ERR-ORD-507",
        Description: "An order must contain at least one item"
    );

    public static Error OrderBelongsToAnotherEstablishment => new Error(
        Code: "#COMANDA-ERR-ORD-508",
        Description: "This order does not belong to your establishment"
    );
}