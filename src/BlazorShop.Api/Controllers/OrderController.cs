using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetOrders()
    {
        var result = await _orderService.GetOrders();
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrdersDetails(int orderId)
    {
        var result = await _orderService.GetOrderDetails(orderId);
        return Ok(result);
    }
}