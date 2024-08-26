using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AddressController(IAddressService addressService) : ControllerBase
{
    private readonly IAddressService _addressService = addressService;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<Address>>> GetAddress()
    {
        return await _addressService.GetAddress();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Address>>> AddOrUpdateAddress(Address address)
    {
        return await _addressService.AddOrUpdateAddress(address);
    }
}