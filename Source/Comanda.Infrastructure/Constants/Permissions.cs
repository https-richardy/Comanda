namespace Comanda.Infrastructure.Constants;

public static class Permissions
{
    public const string ViewProducts = "http://comanda.com.br/permissions/products/read";
    public const string CreateProduct = "http://comanda.com.br/permissions/products/create";
    public const string UpdateProduct = "http://comanda.com.br/permissions/products/edit";
    public const string DeleteProduct = "http://comanda.com.br/permissions/products/delete";

    public const string ViewOrders = "http://comanda.com.br/permissions/orders/read";
    public const string CreateOrder = "http://comanda.com.br/permissions/orders/create";
    public const string AcceptOrder = "http://comanda.com.br/permissions/orders/accept";
    public const string ChangeOrderStatus = "http://comanda.com.br/permissions/orders/status";
    public const string DeleteOrder = "http://comanda.com.br/permissions/orders/delete";

    public const string SubscribePlan = "http://comanda.com.br/permissions/subscriptions/subscribe";
    public const string ViewSubscription = "http://comanda.com.br/permissions/subscriptions/read";
    public const string CancelSubscription = "http://comanda.com.br/permissions/subscriptions/cancel";
}