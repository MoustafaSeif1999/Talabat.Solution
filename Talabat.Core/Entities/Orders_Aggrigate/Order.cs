using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders_Aggrigate
{
    public class Order : BaseEntity
    {

        public Order()
        {
            
        }


        public Order(string buyeremail , Address address , DeliveryMethod deliveryMethod , ICollection<OrderItems> items ,string paymentIntentid,decimal subtotal)
        {
            BuyerEmail = buyeremail;
            ShippingAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            PaymentIntenedId = paymentIntentid;
            SubTotal = subtotal;
        }


        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }


        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItems> Items { get; set; }

        public decimal SubTotal { get; set; }

        public string PaymentIntenedId { get; set; }


        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;

    }
}
