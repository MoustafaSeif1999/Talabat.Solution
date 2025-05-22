using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Core.Specifications
{
    public class OrderWithPaymentIntentIdSpec : BaseSpecifications<Order>
    {

        public OrderWithPaymentIntentIdSpec(string paymentIntentID)
            :base( item => item.PaymentIntenedId == paymentIntentID )
        {
            
        }
    }
}
