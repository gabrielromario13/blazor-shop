global using BlazorShop.Common.Models;
global using BlazorShop.Common.Requests;
global using BlazorShop.Common.Responses;
global using BlazorShop.Web.Services.AddressService;
global using BlazorShop.Web.Services.AuthService;
global using BlazorShop.Web.Services.CartService;
global using BlazorShop.Web.Services.CategoryService;
global using BlazorShop.Web.Services.OrderService;
global using BlazorShop.Web.Services.ProductService;
global using BlazorShop.Web.Services.ProductTypeService;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;
using BlazorShop.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5256") });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();