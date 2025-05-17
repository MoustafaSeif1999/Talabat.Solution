using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.APIs.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItems, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItems source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["BaseApiUrl"]}{source.Product.PictureUrl}";

            }
            return null;
        }
    }
}
