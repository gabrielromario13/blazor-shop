using Microsoft.AspNetCore.Components;

namespace BlazorShop.Web.Services.OrderService;

public class OrderService(HttpClient client,
    AuthenticationStateProvider authStateProvider,
    NavigationManager navigationManager) : IOrderService
{
    private readonly HttpClient _client = client;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
    private readonly NavigationManager _navigationManager = navigationManager;

    public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
    {
        var result = await _client.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
        return result.Data;
    }

    public async Task<List<OrderOverviewResponse>> GetOrders()
    {
        var result = await _client.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
        return result.Data;
    }

    public async Task<string> PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            var result = await _client.PostAsync("api/payment/checkout", null);
            var url = await result.Content.ReadAsStringAsync();
            return url;
        }
        else
        {
            return "login";
        }
    }
    private async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
}