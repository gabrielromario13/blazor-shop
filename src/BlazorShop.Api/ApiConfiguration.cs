namespace BlazorShop.Api;

public static class ApiConfiguration
{
    public const string UserId = "gabriel.rcosta57@gmail.com";
    public static string ConnectionString { get; set; } = string.Empty;
    public readonly static string CorsPolicyName = "wasm";
}