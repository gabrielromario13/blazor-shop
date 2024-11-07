namespace BlazorShop.Web.Services.CategoryService;

public class CategoryService(HttpClient client) : ICategoryService
{
    private readonly HttpClient _client = client;
    public List<Category> Categories { get; set; } = [];
    public List<Category> AdminCategories { get; set; } = [];

    public event Action OnChange = null!;

    public async Task AddCategory(Category category)
    {
        var response = await _client.PostAsJsonAsync("api/Category/admin", category);
        AdminCategories = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
        await GetCategories();
        OnChange.Invoke();
    }

    public Category CreateNewCategory()
    {
        var newCategory = new Category { IsNew = true, Editing = true };
        AdminCategories.Add(newCategory);
        OnChange.Invoke();
        return newCategory;
    }

    public async Task DeleteCategory(int categoryId)
    {
        var response = await _client.DeleteAsync($"api/Category/admin/{categoryId}");
        AdminCategories = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
        await GetCategories();
        OnChange.Invoke();
    }

    public async Task GetAdminCategories()
    {
        var response = await _client.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category/admin");
        if (response != null && response.Data != null)
            AdminCategories = response.Data;
    }

    public async Task GetCategories()
    {
        var response = await _client.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
        if (response != null && response.Data != null)
            Categories = response.Data;
    }

    public async Task UpdateCategory(Category category)
    {
        var response = await _client.PutAsJsonAsync("api/Category/admin", category);
        AdminCategories = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
        await GetCategories();
        OnChange.Invoke();
    }
}