namespace Evently.API.Infrastructure;

public static class PermissionNames
{
    public const string GetUser = "users:read";
    public const string ModifyUser = "users:update";
    public const string GetEvents = "events:read";
    public const string SearchEvents = "events:search";
    public const string ModifyEvents = "events:update";
    public const string GetTicketTypes = "ticket-types:read";
    public const string ModifyTicketTypes = "ticket-types:update";
    public const string GetCategories = "categories:read";
    public const string ModifyCategories = "categories:update";
    public const string GetCart = "carts:read";
    public const string AddToCart = "carts:add";
    public const string RemoveFromCart = "carts:remove";
    public const string GetOrders = "orders:read";
    public const string CreateOrder = "orders:create";
    public const string GetTickets = "tickets:read";
    public const string CheckInTicket = "tickets:check-in";
    public const string GetEventStatistics = "event-statistics:read";
}
