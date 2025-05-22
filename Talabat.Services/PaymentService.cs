using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;
using Talabat.Core.Repositries;
using Talabat.Core.Services;
using Talabat.Core.Specifications;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService( IConfiguration configuration , IBasketRepository basketRepo , IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];

            var basket = await _basketRepo.GetBasketAsync(BasketId);
            
            if (basket == null) 
                return null;

            var ShippingCost = 0m;

            if ( basket.DeliveryMethodId.HasValue )
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingCost = deliveryMethod.Cost;
                basket.ShippingCost = deliveryMethod.Cost;
            }


            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                if ( item.Price != product.Price )
                {
                    item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if ( string.IsNullOrEmpty(basket.PaymentIntentId) )
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)  basket.Items.Sum(item => item.Quantity * item.Price * 100 ) + (long) ShippingCost * 100 ,
                    Currency = "usd" ,
                    PaymentMethodTypes = new List<string>() { "card"}
                    
                };

                intent =  await service.CreateAsync(options);


                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)ShippingCost * 100,
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepo.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId, bool IsSucceeded)
        {
            var order_spec = new OrderWithPaymentIntentIdSpec(PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(order_spec);

            if ( IsSucceeded )
            {
                order.Status = OrderStatus.PaymentRecived;
            }
            else
            {
                order.Status = OrderStatus.PaymentFaild;
            }

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complate();

            return order;
        }
    }
}
