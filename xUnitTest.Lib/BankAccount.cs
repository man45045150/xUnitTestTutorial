using System;

namespace xUnitTest.Lib
{
    public class BankAccount
    {
        public int Balance { get; set; } = 0;
        private readonly ILog log;
        public BankAccount(ILog log)
        {
            this.log = log;
        }
        public void Deposit(int amount){
            log.Write($"User has deposit {amount}");
            if(amount<0)
                throw new ArgumentException("Deposit amount must be positive.",nameof(amount));
            this.Balance += amount;
        }
        public int Withdraw(int amount){
            log.Write($"User has withdrawn {amount}");
            this.Balance -= amount;
            return this.Balance;
        }

    }
}
