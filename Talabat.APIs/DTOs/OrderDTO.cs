namespace Talabat.APIs.DTOs
{
    public class OrderDTO
    {
        public string BaskitId { get; set; }

        public int DeliveryMethod { get; set; }

        public AdressDTo Address { get; set; }

    }
}
