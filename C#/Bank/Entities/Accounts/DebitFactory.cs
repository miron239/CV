using Banks.Entities.Accounts.Account_processors;
using Banks.Entities.Accounts.Account_processors.Add;
using Banks.Entities.Accounts.Account_processors.CommisionProcessor;
using Banks.Entities.Accounts.Account_processors.Withdraw;

namespace Banks.Entities.Accounts
{
    public class DebitFactory : IAccountFactory
    {
        public IAdd CreateAdd()
        {
            return new DebitAdd();
        }

        public IWithdraw CreateWithdraw()
        {
            return new DebitWithdraw();
        }

        public ICommisionProcessor CreateCommisionProcessor()
        {
            return new DebitCommisionStrategy();
        }
    }
}