using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;
using Talabat.Core.Repositries;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService( IBasketRepository basketRepo, IUnitOfWork unitOfWork )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }



        public async Task<Order> CreateOrder(string BuyerEmail, string BasketId, Address ShippingAddress, int DeliveryMethodId)
        {
            var UserBasket = await _basketRepo.GetBasketAsync(BasketId);

            var OrderItems = new List<OrderItems>();

            foreach (var item in UserBasket.Items)
            {
                var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var ProductOrdered = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);

                var orderItem = new OrderItems(ProductOrdered, Product.Price, item.Quantity);
                OrderItems.Add(orderItem);
            }


            var subtotal = OrderItems.Sum(item => item.Quantity * item.Price);

            var DelveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, ShippingAddress, DelveryMethod, OrderItems, subtotal);

            // add order at Db 

            await _unitOfWork.Repository<Order>().CreateAsync(Order);

            // save changes

            int result = await _unitOfWork.Complate();

            if (result <= 0)
                return null;

            return Order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var Dmethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return Dmethods;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            var order_spec = new OrderWithOrderItamsAndDeliveryMethodSpec(OrderId,BuyerEmail);

            var Order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(order_spec);

            return Order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var order_spec = new OrderWithOrderItamsAndDeliveryMethodSpec(BuyerEmail);

            var Orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(order_spec);

            return Orders;
        }
    }
}
