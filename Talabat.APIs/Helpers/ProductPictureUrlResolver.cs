﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if ( ! string.IsNullOrEmpty(source.PictureUrl) )
            {
                return $"{_configuration["BaseApiUrl"]}{source.PictureUrl}";

            }
            return null;
        }
    }
}
