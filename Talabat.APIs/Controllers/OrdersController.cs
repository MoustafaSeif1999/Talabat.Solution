using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using StackExchange.Redis;

//using StackExchange.Redis;
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
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder( OrderDTO orderDTO )
        {
            var Buyermail = User.FindFirstValue(ClaimTypes.Email);

            var OrderAddress =  _mapper.Map<AdressDTo,Address>(orderDTO.Address);
            var order = await _orderRepo.CreateOrder(Buyermail,orderDTO.BaskitId,OrderAddress,orderDTO.DeliveryMethod);

            if (order == null)
                return BadRequest(new ApiResponse(400));

            var ReturnOrder = _mapper.Map<Order , OrderToReturnDTO>(order);

            return Ok(ReturnOrder);

        }


        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
        {
            var UserMail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderRepo.GetOrdersForUserAsync(UserMail);

            var ReturnOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(orders);

            return Ok(ReturnOrders);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id)
        {
            var UserMail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderRepo.GetOrderByIdForUserAsync(id,UserMail);

            if (order == null)
                return BadRequest(new ApiResponse(400));

            var ReturnOrder = _mapper.Map <Order,OrderToReturnDTO >(order);

            return Ok(ReturnOrder);

        }


        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DMethods = await _orderRepo.GetDeliveryMethodsAsync();

            return Ok(DMethods);
        }



    }
}
