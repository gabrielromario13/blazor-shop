using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypeController(IProductTypeService productTypeService) : ControllerBase
{
    private readonly IProductTypeService _productTypeService = productTypeService;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ProductType>>>> GetProductTypes()
    {
        var response = await _productTypeService.GetProductTypes();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<ProductType>>>> AddProductType(ProductType productType)
    {
        var response = await _productTypeService.AddProductType(productType);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<ProductType>>>> UpdateProductType(ProductType productType)
    {
        var response = await _productTypeService.UpdateProductType(productType);
        return Ok(response);
    }
}