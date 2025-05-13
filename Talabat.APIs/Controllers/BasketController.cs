using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController( IBasketRepository basketRepo )
        {
            _basketRepo = basketRepo;
        }


        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasketById([FromQuery]string id)
        {
            var Basket = await _basketRepo.GetBasketAsync(id);
            return Ok(Basket ?? new CustomerBasket(id));
        }


        [HttpPost]
        
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var UpdatedOrCreatedBasket = await _basketRepo.UpdateBasketAsync(basket);
            return Ok(UpdatedOrCreatedBasket);
        }

        [HttpDelete]

        public async Task DeleteBasket (string Id)
        {
           await _basketRepo.DeleteBasket(Id);
        }




    }
}
