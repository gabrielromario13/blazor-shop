namespace BlazorShop.Web.Services.ProductTypeService;

public class ProductTypeService(HttpClient client) : IProductTypeService
{
    private readonly HttpClient _client = client;
    public List<ProductType> ProductTypes { get; set; } = [];
    public event Action OnChange = null!;

    public async Task AddProductType(ProductType productType)
    {
        var response = await _client.PostAsJsonAsync("api/producttype", productType);
        ProductTypes = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
        OnChange.Invoke();
    }

    public ProductType CreateNewProductType()
    {
        var newProductType = new ProductType { IsNew = true, Editing = true };

        ProductTypes.Add(newProductType);
        OnChange.Invoke();
        return newProductType;
    }

    public async Task GetProductTypes()
    {
        var result = await _client
            .GetFromJsonAsync<ServiceResponse<List<ProductType>>>("api/producttype");
        ProductTypes = result.Data;
    }

    public async Task UpdateProductType(ProductType productType)
    {
        var response = await _client.PutAsJsonAsync("api/producttype", productType);
        ProductTypes = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
        OnChange.Invoke();
    }
}