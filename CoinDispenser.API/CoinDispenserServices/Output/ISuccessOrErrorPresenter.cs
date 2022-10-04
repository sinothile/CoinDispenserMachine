namespace CoinDispenserServices.Output
{
    public interface ISuccessOrErrorPresenter<in TSuccess, in TError> : IErrorPresenter<TError>
    {
        public void Success(TSuccess successResult);
    }
}
