using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Orders_Aggrigate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderRepo;
        private readonly IMapper _mapper;

        public OrdersController( IOrderService orderRepo , IMapper mapper )
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder( OrderDTO orderDTO )
        {
            var Buyermail = User.FindFirstValue(ClaimTypes.Email);

            var OrderAddress =  _mapper.Map<AdressDTo,Address>(orderDTO.Address);
            var order = await _orderRepo.CreateOrder(Buyermail,orderDTO.BaskitId,OrderAddress,orderDTO.DeliveryMethod);

            if (order == null)
                return BadRequest(new ApiResponse(400));

            return Ok(order);

        }

    }
}
