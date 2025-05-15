using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController( IBasketRepository basketRepo , IMapper mapper )
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasketById([FromQuery]string id)
        {
            var Basket = await _basketRepo.GetBasketAsync(id);
            return Ok(Basket ?? new CustomerBasket(id));
        }


        [HttpPost]
        
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO,CustomerBasket>(basket);
            var UpdatedOrCreatedBasket = await _basketRepo.UpdateBasketAsync(mappedBasket);
            return Ok(UpdatedOrCreatedBasket);
        }

        [HttpDelete]

        public async Task DeleteBasket (string Id)
        {
           await _basketRepo.DeleteBasket(Id);
        }




    }
}
