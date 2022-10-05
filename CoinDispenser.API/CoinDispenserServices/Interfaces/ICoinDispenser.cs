using CoinDispenserServices.Model;
using CoinDispenserServices.Output;

namespace CoinDispenserServices.Interfaces
{
    public interface ICoinDispenser
    {
        void GetMinimumCoinChange(int[] coins, int amount, ISuccessOrErrorActionResultPresenter<int?, Response> presenter);
    }
}   
        