using System;
using Xunit;
using xUnitTest.Lib;

namespace xUnitTest.Test
{
    public class UnitTestBankAccount
    {
        const int init = 200;
        BankAccount ba = new BankAccount(init);
        public  UnitTestBankAccount()
        {

        }
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void BankAccountShouldIncreaseOnPositionDeposit(int amount){
            
            ba.Deposit(amount);
            Assert.True(ba.Balance>init);
            Assert.True(ba.Balance>0);
        }
        [Theory]
        [InlineData(-100)]
        public void BankAccountShouldThrowOnNonPositiveAmount(int amount){
            Action act = () => ba.Deposit(amount);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
            Assert.Equal(1,1);
            
        }
        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
