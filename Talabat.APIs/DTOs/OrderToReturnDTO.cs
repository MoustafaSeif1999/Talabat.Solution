using System.Collections.Generic;
using System;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.APIs.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }


        //public ICollection<OrderItems> Items { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; }


        public decimal SubTotal { get; set; }

        public string PaymentIntenedId { get; set; }


        public decimal Total { get; set; }

    }
}
