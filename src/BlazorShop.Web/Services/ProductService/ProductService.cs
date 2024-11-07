namespace BlazorShop.Web.Services.ProductService;

public class ProductService(HttpClient client) : IProductService
{
    private readonly HttpClient _client = client;
    public List<Product> Products { get; set; } = [];
    public string Message { get; set; } = "Loading products...";
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public string LastSearchText { get; set; } = string.Empty;
    public List<Product> AdminProducts { get; set; } = [];

    public event Action ProductsChanged = null!;

    public async Task<Product> CreateProduct(Product product)
    {
        var result = await _client.PostAsJsonAsync("api/product", product);
        var newProduct = (await result.Content
            .ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        return newProduct;
    }

    public async Task DeleteProduct(Product product)
    {
        var result = await _client.DeleteAsync($"api/product/{product.Id}");
    }

    public async Task GetAdminProducts()
    {
        var result = await _client
            .GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/admin");
        AdminProducts = result.Data;
        CurrentPage = 1;
        PageCount = 0;
        if (AdminProducts.Count == 0)
            Message = "No products found.";
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _client.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
        return result;
    }

    public async Task GetProducts(string? categoryUrl = null)
    {
        try
        {
            var result = categoryUrl is null ?
                await _client.GetFromJsonAsync<ServiceResponse<List<Product>?>>("api/product/featured") :
                await _client.GetFromJsonAsync<ServiceResponse<List<Product>?>>($"api/product/category/{categoryUrl}");

            if (result is not null && result.Data is not null)
            {
                Products = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No products found";

            ProductsChanged.Invoke();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<string>> GetProductSearchSuggestions(string searchText)
    {
        var result = await _client
            .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
        return result.Data;
    }

    public async Task SearchProducts(string searchText, int page)
    {
        LastSearchText = searchText;
        var result = await _client
             .GetFromJsonAsync<ServiceResponse<ProductSearchResponse>>($"api/product/search/{searchText}/{page}");
        if (result != null && result.Data != null)
        {
            Products = result.Data.Products;
            CurrentPage = result.Data.CurrentPage;
            PageCount = result.Data.Pages;
        }
        if (Products.Count == 0) Message = "No products found.";
        ProductsChanged?.Invoke();
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var result = await _client.PutAsJsonAsync($"api/product", product);
        var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
        return content.Data;
    }
}