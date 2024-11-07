using BlazorShop.Common.Models;

namespace BlazorShop.Common.Responses;

public class ProductSearchResponse
{
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
    public List<Product> Products { get; set; } = [];
}