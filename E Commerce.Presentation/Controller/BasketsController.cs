using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DTOS.Basket;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController:ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket= await _basketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatebasket(BasketDto basket)
        {
            var Basket =await _basketService.CreateOrUpdateAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id )
        {
            var result = await _basketService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
