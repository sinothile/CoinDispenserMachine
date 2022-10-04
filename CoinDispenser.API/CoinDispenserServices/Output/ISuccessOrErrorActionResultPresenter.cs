using Microsoft.AspNetCore.Mvc;

namespace CoinDispenserServices.Output
{
    public interface ISuccessOrErrorActionResultPresenter<in TSuccess, in TError> : ISuccessOrErrorPresenter<TSuccess, TError>
    {
        public IActionResult Render();
    }
}
