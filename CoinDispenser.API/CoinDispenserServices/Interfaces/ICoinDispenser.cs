using CoinDispenserServices.Model;
using CoinDispenserServices.Output;

namespace CoinDispenserServices.Interfaces
{
    public interface ICoinDispenser
    {
        void GetMinimumCoins(int[] coins, int amount, ISuccessOrErrorActionResultPresenter<int?, ErrorMessageDTO> presenter);
        int GetMinimumCoins(int[] coins, int amount);
    }
}   
