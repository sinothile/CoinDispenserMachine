using CoinDispenserServices.Interfaces;
using CoinDispenserServices.Model;
using CoinDispenserServices.Output;
using Microsoft.AspNetCore.Mvc;

namespace CoinDispenser.API.Controllers
{
    [ApiController]
    [Route("api/coin")]
    public class CoinDispenserController : ControllerBase
    {
        private readonly ICoinDispenser _coinDispenser;
        private readonly ISuccessOrErrorActionResultPresenter<int?, ErrorMessageDTO> _presenter;
        public CoinDispenserController(ICoinDispenser coinDispenser, ISuccessOrErrorActionResultPresenter<int?, ErrorMessageDTO> presenter)
        {
            _coinDispenser = coinDispenser;
            _presenter = presenter;
        }

        [HttpPost]
        public IActionResult GetChange([FromBody] Request request)
        {
            _coinDispenser.GetMinimumCoins(request.Coins, request.Amount, _presenter);
            return _presenter.Render();
        } 
        
        [HttpPost]
        [Route("coin_dispenser")]
        public IActionResult GetChangeTwo([FromBody] Request request)
        {
           var results= _coinDispenser.GetMinimumCoins(request.Coins, request.Amount);
           if (results > 0)
           {
               return Ok(results);
           }
           else
           {
               return BadRequest(new ErrorMessageDTO
               {
                   Message = request.Amount == 0 ? "Sorry, there is no change for given amount" : "Sorry, there are no coins available"
               });
           }
        }
    }
}
