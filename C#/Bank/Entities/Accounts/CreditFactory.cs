using Banks.Entities.Accounts.Account_processors;
using Banks.Entities.Accounts.Account_processors.Add;
using Banks.Entities.Accounts.Account_processors.CommisionProcessor;
using Banks.Entities.Accounts.Account_processors.Withdraw;

namespace Banks.Entities.Accounts
{
    public class CreditFactory : IAccountFactory
    {
        public IAdd CreateAdd()
        {
            return new CreditAdd();
        }

        public IWithdraw CreateWithdraw()
        {
            return new CreditWithdraw();
        }

        public ICommisionProcessor CreateCommisionProcessor()
        {
            return new CreditCommisionStrategy();
        }
    }
}