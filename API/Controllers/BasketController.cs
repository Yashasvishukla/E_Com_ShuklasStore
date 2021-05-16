using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController: BaseApiController
    {
        private readonly IBasketRepository _repo;
        public BasketController(IBasketRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var data = await _repo.GetBasketAsync(basketId);
            if(data == null)
            {
                return Ok(new CustomerBasket(basketId));
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _repo.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            var deletedBasket = await _repo.DeleteBasketAsync(basketId);
            return Ok(deletedBasket);
        }

    }
}