using System;
using Banks.Entities.Accounts;

namespace Banks.Entities.Transaction
{
    public class Transaction
    {
        public Transaction(Account senderAccount, Account addresseeAccount, decimal amount)
        {
            SenderAccount = senderAccount;
            AddresseeAccount = addresseeAccount;
            Amount = amount;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Account SenderAccount { get; private set; }
        public Account AddresseeAccount { get; private set; }
        public decimal Amount { get; private set; }
    }
}