using Banks.Entities.Bank;
using Banks.Entities.Client;
using Banks.Tools;

namespace Banks.Entities.Accounts.Account_processors.Withdraw
{
    public class CreditWithdraw : IWithdraw
    {
        public void Withdraw(ClientInfo clientInfo, BankInfo bankInfo, CashKeeper cash, decimal amount)
        {
            if (cash.Cash - amount < bankInfo.CreditLimit)
            {
                throw new BanksExceptions("Invalid sum of transaction");
            }

            if (clientInfo.IsVerified == false && amount > bankInfo.NotReliableTransaction)
            {
                throw new BanksExceptions("You cant add this amount to non verified");
            }

            cash.WithdrawCash(amount);
        }
    }
}