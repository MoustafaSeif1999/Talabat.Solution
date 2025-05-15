using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericReopositiory<Product> _ProductsRepo;
        private readonly IGenericReopositiory<ProductBrand> _brandsRepo;
        private readonly IGenericReopositiory<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController( IGenericReopositiory<Product> genericReopositiory ,
          IGenericReopositiory<ProductBrand> BrandsRepo , IGenericReopositiory<ProductType> TypesRepo
            , IMapper mapper )
        {
            _ProductsRepo = genericReopositiory;
            _brandsRepo = BrandsRepo;
            _typeRepo = TypesRepo;
            _mapper = mapper;
        }

        //[Authorize/*(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO> >> GetAllProducts([FromQuery]ProductSpecParams specParams)
        {
            var product_spec = new ProductWithBrandAndTypeSpec(specParams);
            var products = await _ProductsRepo.GetAllWithSpecAsync(product_spec);
            var Products_View = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
            var count_Spec = new ProductWithFiltersForCountSpec(specParams);
            var count = await _ProductsRepo.GetCountAsync(count_Spec);
            var Standered_Response = new Pagination<ProductToReturnDTO>(specParams.PageSize, specParams.PageIndex,count, Products_View);
            return Ok(Standered_Response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
        {
            var Product_Spec = new ProductWithBrandAndTypeSpec(id);
            var Product = await _ProductsRepo.GetByIdWithSpecAsync(Product_Spec);
            var Products_View = _mapper.Map<Product, ProductToReturnDTO>(Product);
            if (Products_View == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok( Products_View );
        }


        [HttpGet("types")]
        public async Task < ActionResult <IReadOnlyList<ProductType>> >GetAllTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            
            return Ok(Types);
        }


        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brandsRepo.GetAllAsync();

            return Ok(Brands);
        }

    }
}
