using Banks.Entities.Accounts.Account_processors;
using Banks.Entities.Accounts.Account_processors.Add;
using Banks.Entities.Accounts.Account_processors.CommisionProcessor;
using Banks.Entities.Accounts.Account_processors.Withdraw;

namespace Banks.Entities.Accounts
{
    public interface IAccountFactory
    {
        public IAdd CreateAdd();
        public IWithdraw CreateWithdraw();
        public ICommisionProcessor CreateCommisionProcessor();
    }
}