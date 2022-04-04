using Banks.Entities.Bank;
using Banks.Entities.Client;

namespace Banks.Entities.Accounts.Account_processors.Add
{
    public interface IAdd
    {
        void AddMoney(ClientInfo clientInfo, BankInfo bankInfo, CashKeeper cash, decimal amount);
    }
}