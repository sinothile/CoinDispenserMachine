using Microsoft.AspNetCore.Mvc;

namespace CoinDispenserServices.Output
{
    public class SuccessOrErrorRestPresenter<TSucces, TError> : ISuccessOrErrorActionResultPresenter<TSucces, TError>
    {
        private TError _error;
        private TSucces _success;

        public void Error(TError error)
        {
            _error = error;
        }

        public void Success(TSucces successResult)
        {
            _success = successResult;
        }

        public IActionResult Render()
        {
            if (HasError() && HasSuccess())
            {
                throw CreateMultipleResultsException();
            }

            if (HasError())
            {
                return new BadRequestObjectResult(_error);
            }

            if (!HasSuccess())
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(_success);
        }

        private bool HasError()
        {
            return _error != null;
        }

        private bool HasSuccess()
        {
            return _success != null;
        }

        private PresenterException CreateMultipleResultsException()
        {
            return new PresenterException($"{nameof(SuccessOrErrorRestPresenter<TSucces, TError>)} was unable to render because it was given both a success and error results");
        }
    }
}
