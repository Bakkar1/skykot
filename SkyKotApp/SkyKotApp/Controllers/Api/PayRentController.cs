using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SkyKotApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayRentController : ControllerBase
    {
        public void Test()
        {
            //// Set your X-API-KEY with the API key from the Customer Area.
            //var client = new Client("YOUR_X-API-KEY", Environment.Test);
            //var checkout = new Checkout(client);

            //var amount = new Model.Checkout.Amount("EUR", 1000);
            //var details = new Model.Checkout.DefaultPaymentMethodDetails
            //{
            //    Type = "sepadirectdebit",
            //    RecurringDetailReference = "7219687191761347"
            //};
            //var paymentsRequest = new Model.Checkout.PaymentRequest
            //{
            //    Reference = "YOUR_ORDER_NUMBER",
            //    Amount = amount,
            //    ReturnUrl = @"https://your-company.com/checkout?shopperOrder=12xy..",
            //    MerchantAccount = "YOUR_MERCHANT_ACCOUNT",
            //    ShopperReference = "YOUR_UNIQUE_SHOPPER_ID_IOfW3k9G2PvXFu2j",
            //    RecurringProcessingModel = "Subscription",
            //    ShopperInteraction = "ContAuth",
            //    PaymentMethod = details
            //};

            //var paymentResponse = checkout.Payments(paymentsRequest);
        }
    }
}
