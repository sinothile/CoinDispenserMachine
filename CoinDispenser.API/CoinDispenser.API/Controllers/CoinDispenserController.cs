using CoinDispenserServices.Interfaces;
using CoinDispenserServices.Model;
using CoinDispenserServices.Output;
using Microsoft.AspNetCore.Mvc;

namespace CoinDispenser.API.Controllers
{
    [ApiController]
    [Route("api/coin-dispenser")]
    public class CoinDispenserController : ControllerBase
    {
        private readonly ICoinDispenser _coinDispenser;
        private readonly ISuccessOrErrorActionResultPresenter<int?, Response> _presenter;
        public CoinDispenserController(ICoinDispenser coinDispenser, ISuccessOrErrorActionResultPresenter<int?, Response> presenter)
        {
            _coinDispenser = coinDispenser;
            _presenter = presenter;
        }

        [HttpPost]
        public IActionResult GetChange([FromBody] Request request)
        {
            _coinDispenser.GetMinimumCoinChange(request.Coins, request.Amount, _presenter);
            return _presenter.Render();
        }

    }
}
