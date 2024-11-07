global using BlazorShop.Api.Data;
global using BlazorShop.Api.Services.AddressService;
global using BlazorShop.Api.Services.AuthService;
global using BlazorShop.Api.Services.CartService;
global using BlazorShop.Api.Services.CategoryService;
global using BlazorShop.Api.Services.OrderService;
global using BlazorShop.Api.Services.PaymentService;
global using BlazorShop.Api.Services.ProductService;
global using BlazorShop.Api.Services.ProductTypeService;
global using BlazorShop.Common.Models;
global using BlazorShop.Common.Requests;
global using BlazorShop.Common.Responses;
global using Microsoft.EntityFrameworkCore;
using BlazorShop.Api;
using BlazorShop.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(ApiConfiguration.CorsPolicyName);

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();