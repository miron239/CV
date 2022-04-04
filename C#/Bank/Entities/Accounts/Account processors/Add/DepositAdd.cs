using Banks.Entities.Bank;
using Banks.Entities.Client;
using Banks.Tools;

namespace Banks.Entities.Accounts.Account_processors.Add
{
    public class DepositAdd : IAdd
    {
        public void AddMoney(ClientInfo clientInfo, BankInfo bankInfo, CashKeeper cash, decimal amount)
        {
            if (amount <= 0)
            {
                throw new BanksExceptions("Invalid sum of transaction");
            }

            if (clientInfo.IsVerified == false && amount > bankInfo.NotReliableTransaction)
            {
                throw new BanksExceptions("You cant add this amount to non verified");
            }

            if (cash.CanUseAccount == false)
            {
                throw new BanksExceptions("cant withdraw deposit before");
            }

            cash.SetCanUseAccount(false);

            cash.AddCash(amount);
        }
    }
}