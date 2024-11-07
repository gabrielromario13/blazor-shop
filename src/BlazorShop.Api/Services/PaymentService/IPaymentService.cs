using Stripe.Checkout;

namespace BlazorShop.Api.Services.PaymentService;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSession();
    Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request);
}