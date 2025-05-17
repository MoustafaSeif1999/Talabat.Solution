using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Description { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Basket must have one Product at Least")]
        public int Quantity { get; set; }

        [Required] 
        [Range(01,double.MaxValue,ErrorMessage ="Price must be gretter than Zero")]
        public decimal Price { get; set; }
        [Required] 
        public string PictureUrl { get; set; }
        [Required] 

        public string Brand { get; set; }

        [Required] 
        public string Type { get; set; }
    }
}