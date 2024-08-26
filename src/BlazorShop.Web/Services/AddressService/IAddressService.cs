namespace BlazorShop.Web.Services.AddressService;

public interface IAddressService
{
    Task<Address> GetAddress();
    Task<Address> AddOrUpdateAddress(Address address);
}