using System;

namespace xUnitTest.Lib
{
    public class BankAccount
    {
        public int Balance { get; set; } = 0;
        public BankAccount(int startingBalance)
        {
            this.Balance = startingBalance;    
        }
        public void Deposit(int amount){
            if(amount<0)
                throw new ArgumentException("Deposit amount must be positive.",nameof(amount));
            this.Balance += amount;
        }
        public void Withdraw(int amount){
            this.Balance -= amount;
        }

    }
}
