using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Core.Specifications
{
    public class OrderWithOrderItamsAndDeliveryMethodSpec : BaseSpecifications<Order>
    {

        public OrderWithOrderItamsAndDeliveryMethodSpec()
        {
            
        }

        public OrderWithOrderItamsAndDeliveryMethodSpec(string BuyerMail)
            :base( O => O.BuyerEmail == BuyerMail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDes(O => O.OrderDate);
        }


        public OrderWithOrderItamsAndDeliveryMethodSpec( int OrderId, string BuyerMail)
            : base(O => O.BuyerEmail == BuyerMail && O.Id == OrderId )
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

        }


    }
}
