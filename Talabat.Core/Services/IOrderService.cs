﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {

        Task<Order> CreateOrder(string BuyerEmail, string BasketId ,Address ShippingAddress ,int DeliveryMethodId );

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);

        Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();  
    }
}
