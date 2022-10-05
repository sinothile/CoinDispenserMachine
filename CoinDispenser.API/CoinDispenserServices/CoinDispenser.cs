using CoinDispenserServices.Interfaces;
using CoinDispenserServices.Model;
using CoinDispenserServices.Output;

namespace CoinDispenserServices
{
    public class CoinDispenser : ICoinDispenser
    {
        public void GetMinimumCoinChange(int[] coins, int amount, ISuccessOrErrorActionResultPresenter<int?, Response> presenter)
        {
            try
            {
                // if the amount is 0 or no coins, there are 0 ways to make change, so return fast
                if (AmountZeroOrNoCoins(coins, amount))
                {
                    presenter.Error(new Response
                    {
                        Message = amount == 0 ? "Sorry, there is no change for given amount" : "Sorry, there are no coins available"
                    });
                    return;
                }
                // create an array to hold the minimum number of coins to make each amount
                // amount + 1 so that you will have indices from 0 to amount in the array
                var change = new int[amount + 1];
                // there are 0 ways to make amount 0 with positive coin values
                change[0] = 0;

                //fill all the elements with the max value starting from index 1
                Array.Fill(change, int.MaxValue, 1, amount);

                for (var i = 1; i <= amount; i++)
                {
                    // look at one coin at a time
                    foreach (var coin in coins)
                    {
                        // if the coin is greater than the amount then continue 
                        if (coin > i) continue;
                        var currentCount = change[i - coin];

                        // Check for INT_MAX to avoid overflow and see if result can minimized
                        if (currentCount != int.MaxValue && currentCount + 1 < change[i])
                        {
                            change[i] = currentCount + 1;
                        }
                    }
                }

                // if the value remains int max value, it means that no coin combination can make that amount
                if (change[amount] == int.MaxValue)
                {
                    presenter.Error(new Response
                    {
                        Message = "There is no coin combination to make that amount"
                    });
                    return;
                }

                presenter.Success(change[amount]);

            }
            catch (Exception)
            {
                presenter.Error(new Response
                {
                    Message = "Invalid input"
                });
            }
        }

        private static bool AmountZeroOrNoCoins(int[] coins, int amount)
        {
            return amount == 0 || coins.Length == 0;
        }
    }
}