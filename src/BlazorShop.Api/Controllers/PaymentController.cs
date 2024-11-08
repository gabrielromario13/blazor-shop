﻿using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("checkout")]
    public async Task<ActionResult<string>> CreateCheckoutSession()
    {
        var session = await _paymentService.CreateCheckoutSession();
        return Ok(session.Url);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> FulfillOrder()
    {
        var response = await _paymentService.FulfillOrder(Request);
        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response);
    }
}