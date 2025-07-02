namespace Comanda.Shared.Configuration;

public sealed class ClientSettings
{
    public string CheckoutSuccessUrl { get; set; } = default!;
    public string CheckoutCancelUrl { get; set; } = default!;
}