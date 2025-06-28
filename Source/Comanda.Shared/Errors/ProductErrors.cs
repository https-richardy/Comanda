namespace Comanda.Shared.Errors;

public static class ProductErrors
{
    public static Error ProductNotFound => new Error(
        Code: "#COMANDA-ERR-PRD-404",
        Description: "Product not found, this product does not exist"
    );

    public static Error ProductAlreadyExists => new Error(
        Code: "#COMANDA-ERR-PRD-409",
        Description: "A product with the same name has already been found"
    );

    public static Error ProductNotAvailableForDelivery => new Error(
        Code: "#COMANDA-ERR-PRD-461",
        Description: "The product is not available for delivery orders"
    );
}