using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregation;
using Talabat.Core.Services;
using TALABAT.API.DTO_s;
using TALABAT.API.Errors;

namespace TALABAT.API.Controllers
{

    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DelvieryMethodId, MappedAddress);
            if (Order is null) return BadRequest(new ApiResponse(400,"There is a proplem with ur order"));

            return Ok(Order);

        }

    }
}
