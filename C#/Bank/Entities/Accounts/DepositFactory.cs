using Banks.Entities.Accounts.Account_processors;
using Banks.Entities.Accounts.Account_processors.Add;
using Banks.Entities.Accounts.Account_processors.CommisionProcessor;
using Banks.Entities.Accounts.Account_processors.Withdraw;

namespace Banks.Entities.Accounts
{
    public class DepositFactory : IAccountFactory
    {
        public IAdd CreateAdd()
        {
            return new DepositAdd();
        }

        public IWithdraw CreateWithdraw()
        {
            return new DepositWithdraw();
        }

        public ICommisionProcessor CreateCommisionProcessor()
        {
            return new DepositCommisionStrategy();
        }
    }
}