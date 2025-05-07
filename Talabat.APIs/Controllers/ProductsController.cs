using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericReopositiory<Product> _genericReopositiory;

        public ProductsController( IGenericReopositiory<Product> genericReopositiory  )
        {
            _genericReopositiory = genericReopositiory;
        }

        [HttpGet]
        public async Task<ActionResult< IEnumerable<Product> >> GetAllProducts()
        {
            var products = await _genericReopositiory.GetAllAsync(); 
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Product = await _genericReopositiory.GetByIdAsync(id);
            return Ok(Product);
        }

    }
}
