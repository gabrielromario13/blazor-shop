namespace BlazorShop.Web.Services.AddressService;

public class AddressService(HttpClient client) : IAddressService
{
    private readonly HttpClient _client = client;

    public async Task<Address> AddOrUpdateAddress(Address address)
    {
        var response = await _client.PostAsJsonAsync("api/address", address);
        return response.Content
            .ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
    }

    public async Task<Address> GetAddress()
    {
        var response = await _client
            .GetFromJsonAsync<ServiceResponse<Address>>("api/address");
        return response.Data;
    }
}