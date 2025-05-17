using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;
using Talabat.Core.Repositries;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericReopositiory<Product> _productRepo;
        private readonly IGenericReopositiory<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericReopositiory<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepo ,
            IGenericReopositiory<Product> productRepo,
            IGenericReopositiory<DeliveryMethod> deliveryMethodRepo,
            IGenericReopositiory<Order> orderRepo
            )
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _orderRepo = orderRepo;
        }



        public async Task<Order> CreateOrder(string BuyerEmail, string BasketId, Address ShippingAddress, int DeliveryMethodId)
        {
            var UserBasket = await _basketRepo.GetBasketAsync( BasketId );

            var OrderItems = new List<OrderItems>();

            foreach (var item in UserBasket.Items)
            {
                var Product = await _productRepo.GetByIdAsync(item.Id);

                var ProductOrdered = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);

                var orderItem = new OrderItems(ProductOrdered, Product.Price, item.Quantity);
                OrderItems.Add(orderItem);
            }


            var subtotal = OrderItems.Sum( item => item.Quantity * item.Price );

            var DelveryMethod = await _deliveryMethodRepo.GetByIdAsync(DeliveryMethodId);

            var Order = new Order( BuyerEmail , ShippingAddress ,DelveryMethod,OrderItems,subtotal);

            // add order at Db 

            await _orderRepo.CreateAsync(Order);

            // save changes

            return Order;

        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
