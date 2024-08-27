using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var result = await _categoryService.GetCategories();
        return Ok(result);
    }

    [HttpGet("admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAdminCategories()
    {
        var result = await _categoryService.GetAdminCategories();
        return Ok(result);
    }

    [HttpDelete("admin/{id}")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategory(id);
        return Ok(result);
    }

    [HttpPost("admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategory(Category category)
    {
        var result = await _categoryService.AddCategory(category);
        return Ok(result);
    }

    [HttpPut("admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> UpdateCategory(Category category)
    {
        var result = await _categoryService.UpdateCategory(category);
        return Ok(result);
    }
}