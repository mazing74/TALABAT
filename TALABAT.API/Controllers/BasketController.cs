using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;
using TALABAT.API.Errors;

namespace TALABAT.API.Controllers
{
    [Authorize]
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }


        [HttpGet("{BasketId}")]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerBasket>> GetAllAsync(string BasketId)
        {
            var Basket = await _basketRepository.GetBasketAsync(BasketId);

            return Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);

        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerBasket>> UpdateAsync(CustomerBasket Basket)
        {
            var CreatedOrUbdatedBasekt = await _basketRepository.UbdateBasketAsync(Basket);

            if (CreatedOrUbdatedBasekt is null)
                return BadRequest(new ApiResponse(400));

            return Ok(CreatedOrUbdatedBasekt);
        }



        [HttpDelete("{BasketId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<bool>> DeleteAsync(string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }












    }
}
