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

    }
}
