namespace BlazorShop.Web.Services.OrderService;

public interface IOrderService
{
    Task<string> PlaceOrder();
    Task<List<OrderOverviewResponse>> GetOrders();
    Task<OrderDetailsResponse> GetOrderDetails(int orderId);
}