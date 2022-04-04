using Banks.Entities.Bank;
using Banks.Entities.Client;

namespace Banks.Entities.Accounts.Account_processors.Withdraw
{
    public interface IWithdraw
    {
        public void Withdraw(ClientInfo clientInfo, BankInfo bankInfo, CashKeeper cash, decimal amount);
    }
}