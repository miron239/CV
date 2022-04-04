using Banks.Entities.Accounts;

namespace Banks.Entities.Bank
{
    public class TransactionProcessor
    {
        public TransactionProcessor()
        {
        }

        public void ConductTransaction(Transaction.Transaction transaction)
        {
            transaction.SenderAccount.WithdrawMoney(transaction.Amount);
            transaction.AddresseeAccount.AddMoney(transaction.Amount);
        }
    }
}