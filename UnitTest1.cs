using System;
using System.Security.Principal;
using Newtonsoft.Json.Linq;

namespace BankAccountXUnit
{
    public class UnitTest1
    {
        BankAccount _account1;
        BankAccount _account2;


        public UnitTest1()
        {
            _account1 = new BankAccount();
            _account1.Deposit(5000);
            
            
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-100)]
        [InlineData(-50)]
        public void NegativeDepositAmountThrowsError(int depositAmount)
        {
            //Action             
            var exception = Assert.Throws<ArgumentException>(() =>
            _account1.Deposit(depositAmount));

            Assert.Equal(exception.Message, "Deposit amount must be positive");
            
        }
        [Theory]
        [InlineData(50, 5050)]
        [InlineData(1000, 6000)]
        public void ValidAmountForDepositIncreasesBalance(int amount, int expectedBalance)
        {
            //action
            _account1.Deposit(amount);

            //Assert
            Assert.Equal(_account1.GetBalance(), expectedBalance);
        }

        [Theory]
        [InlineData(-500)]
        [InlineData(-5000)]
        [InlineData(-500)]
        public void WithdrawOfNegativeAmountThrowsException(int amount)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            _account1.Withdraw(amount));

            Assert.Equal(exception.Message, "Withdrawal amount must be positive");
            
        }
        [Theory]
        [InlineData(5001)]
        [InlineData(6000)]
        public void WithdrawingMoreThanBalanceThrowsError(int amount)
        {
            var exception = Assert.Throws<InvalidOperationException>(()=>
            _account1.Withdraw(amount));

            Assert.Equal(exception.Message, "Insufficient Funds !");
        }

        [Theory]
        [InlineData(500, 4500)]
        [InlineData(1000, 4000)]
        public void WithdrawingAmountLessThanBalanceDecreasesBalance(int amount, double expectedAmount)
        {
            _account1.Withdraw(amount);
            Assert.Equal(_account1.GetBalance(), expectedAmount);
        }

        [Theory]
        [InlineData(400)]
        [InlineData(4200)]
        public void TransferringInvalidAmountToAccount(int amount)
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () =>
            _account1.Transfer(_account2, amount));

            Assert.Equal(exception.ParamName, "toAccount");
        }
    }
}