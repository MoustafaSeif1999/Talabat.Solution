using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Core.Services
{
    public interface IPaymentServices
    {
        public Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);

        public Task<Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId, bool IsSucceeded);

    }
}
