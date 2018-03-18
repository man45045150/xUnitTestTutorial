using System;
using Moq;
using Xunit;
using xUnitTest.Lib;

namespace xUnitTest.Test
{
    public class UnitTestBankAccount
    {
        const int init = 200;
        BankAccount ba = null;
        public  UnitTestBankAccount()
        {
            ba = new BankAccount(new ConsoleLog());
            ba.Balance = init;
        }
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        [InlineData(400)]
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
        [InlineData(7)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
            
        }
        [Theory]
        [InlineData(100,true,100)]
        [InlineData(50,true,150)]
        [InlineData(200,true,0)]
        public void TestMultipleWithdrawalScenerios(int amountToWithdraw,bool shouldSucceed,int expectedBalance){
            var result = ba.Withdraw(amountToWithdraw); 
            Assert.Equal((result==expectedBalance),shouldSucceed);
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
        [Fact]
        public void DepositTest(){
            var log = new Mock<ILog>();
            ba = new BankAccount(log.Object){Balance = 100};
            ba.Deposit(100);
            Assert.True(ba.Balance==200);
        }
    }
}
