using CoinDispenserServices;
using NUnit.Framework;
using System;
using CoinDispenserServices.Interfaces;
using CoinDispenserServices.Model;
using NSubstitute;
using CoinDispenserServices.Output;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ReceivedExtensions;

namespace CoinDispenserTests
{
    public class CoinDispenserTests
    {   
        [TestCase(new int[] { 25, 10, 5 }, 30, 2)]
        [TestCase(new int[] { 9, 6, 5, 1 }, 13, 3)]
        [TestCase(new int[] { 1, 3, 5, 7 }, 18, 4)]
        public void GetMinimumCoinChange_given_coins_and_amount_should_return_minimum_number_of_coins_to_make_change_for_amount(int[] coins, int amount, int expected)
        {
            //Arrange
            var coinDispenser = CreateCoinDispenser();
            var presenter = CreatePresenter();

            //Act
            coinDispenser.GetMinimumCoinChange(coins, amount, presenter);
            var results = presenter.Render() as OkObjectResult;

            //Assert
            Assert.AreEqual(results.Value, expected);
        }

        [Test]
        public void GetMinimumCoinChange_given_no_coins_and_amount_should_return_error_message()
        {
            //Arrange   
            var coins = new int[] { };
            var amount = 10;
            var expected = new Response { Message = "Sorry, there are no coins available" };
            var coinDispenser = CreateCoinDispenser();
            var presenter = CreatePresenter();

            //Act
            coinDispenser.GetMinimumCoinChange(coins, amount, presenter);
            var results = presenter.Render() as BadRequestObjectResult;
            var error = results?.Value as Response;

            //Assert
            Assert.AreEqual(error.Message, expected.Message);
        }

        [Test]
        public void GetMinimumCoinChange_given_coins_and_amount_of_zero_should_return_error_message()
        {
            //Arrange   
            var coins = new int[] { 3, 4, 5 };
            var amount = 0;
            var expected = new Response {Message = "Sorry, there is no change for given amount" };
            var coinDispenser = CreateCoinDispenser();
            var presenter = CreatePresenter();

            //Act
            coinDispenser.GetMinimumCoinChange(coins, amount, presenter);
            var results = presenter.Render() as BadRequestObjectResult;
            var error = results?.Value as Response;

            //Assert
            Assert.AreEqual(error.Message, expected.Message);
        }

        [Test]
        public void GetMinimumCoinChange_given_coins_and_negative_amount_should_return_message_invalid_input()
        {
            //Arrange           
            var coins = new int[] { 3, 4, 5 };
            var amount = -10;
            var expected = new Response { Message = "Invalid input" };
            var coinDispenser = CreateCoinDispenser();
            var presenter = CreatePresenter();

            //Act
            coinDispenser.GetMinimumCoinChange(coins, amount, presenter);
            var results = presenter.Render() as BadRequestObjectResult;
            var error = results?.Value as Response;

            //Assert
            Assert.AreEqual(error?.Message, expected.Message);
        }

        [Test]
        public void GetMinimumCoinChange_given_negative_coins_and_amount_should_return_message_invalid_input()
        {
            //Arrange           
            var coins = new int[] { -3, -10, -5 };
            var amount = 10;
            var expected = new Response { Message = "Invalid input" };
            var coinDispenser = CreateCoinDispenser();
            var presenter = CreatePresenter();

            //Act
            coinDispenser.GetMinimumCoinChange(coins, amount, presenter);
            var results = presenter.Render() as BadRequestObjectResult;
            var error = results?.Value as Response;

            //Assert
            Assert.AreEqual(error?.Message, expected.Message);
        }

        private static CoinDispenser CreateCoinDispenser()
        {
            return new CoinDispenser();
        }

        private static SuccessOrErrorRestPresenter<int?, Response> CreatePresenter()
        {
            return new SuccessOrErrorRestPresenter<int?, Response>();
        }
    }
}