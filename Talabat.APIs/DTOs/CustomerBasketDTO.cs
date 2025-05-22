using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();

        public string PaymentIntentId { get; set; }

        public string ClientSecret { get; set; }

        public decimal ShippingCost { get; set; }

        public int? DeliveryMethodId { get; set; }

    }
}
