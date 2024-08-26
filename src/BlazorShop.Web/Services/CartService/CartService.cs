using Blazored.LocalStorage;

namespace BlazorShop.Web.Services.CartService;

public class CartService(
    ILocalStorageService localStorage,
    IAuthService authService,
    HttpClient client) : ICartService
{
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly IAuthService _authService = authService;
    private readonly HttpClient _client = client;

    public event Action OnChange = null!;

    public async Task AddToCart(CartItem cartItem)
    {
        if (await _authService.IsUserAuthenticated())
        {
            await _client.PostAsJsonAsync("api/cart/add", cartItem);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            cart ??= [];

            var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId &&
                x.ProductTypeId == cartItem.ProductTypeId);

            if (sameItem is null)
                cart.Add(cartItem);
            else
                sameItem.Quantity += cartItem.Quantity;

            await _localStorage.SetItemAsync("cart", cart);
        }
        await GetCartItemsCount();
    }

    public async Task GetCartItemsCount()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var result = await _client.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
            var count = result.Data;

            await _localStorage.SetItemAsync<int>("cartItemsCount", count);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
        }

        OnChange.Invoke();
    }

    public async Task<List<CartProductResponse>> GetCartProducts()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");
            return response.Data;
        }
        else
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cartItems is null)
                return [];

            var response = await _client.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();

            return cartProducts.Data;
        }

    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        if (await _authService.IsUserAuthenticated())
        {
            await _client.DeleteAsync($"api/cart/{productId}/{productTypeId}");
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart is null)
                return;

            var cartItem = cart.Find(x => x.ProductId == productId
                && x.ProductTypeId == productTypeId);

            if (cartItem is not null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }

    public async Task StoreCartItems(bool emptyLocalCart = false)
    {
        var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (localCart is null)
            return;

        await _client.PostAsJsonAsync("api/cart", localCart);

        if (emptyLocalCart)
            await _localStorage.RemoveItemAsync("cart");
    }

    public async Task UpdateQuantity(CartProductResponse product)
    {
        if (await _authService.IsUserAuthenticated())
        {
            var request = new CartItem
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                ProductTypeId = product.ProductTypeId
            };
            await _client.PutAsJsonAsync("api/cart/update-quantity", request);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart is null)
                return;

            var cartItem = cart.Find(x => x.ProductId == product.ProductId
                && x.ProductTypeId == product.ProductTypeId);
            if (cartItem is not null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}