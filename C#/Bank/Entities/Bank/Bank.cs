using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.Accounts.DepositBids;
using Banks.Entities.Client;
using Banks.Tools;

namespace Banks.Entities.Bank
{
    public class Bank : ISubject
    {
        private readonly List<Transaction.Transaction> _transactions = new List<Transaction.Transaction>();
        private readonly TransactionProcessor _transactionProcessor;
        private readonly List<Account> _accounts = new List<Account>();
        private List<IObserver> _observers = new List<IObserver>();
        public Bank(string name, List<DepositBid> depositBids, decimal creditLimit, decimal commission, decimal notReliableTransaction)
        {
            _transactionProcessor = new TransactionProcessor();
            BankInfo = new BankInfo(depositBids, creditLimit, commission, notReliableTransaction);
        }

        public BankInfo BankInfo { get; private set; }
        public int State { get; set; } = -0;

        public Transaction.Transaction ConductTransaction(Account sender, Account addressee, decimal amount)
        {
            var transaction = new Transaction.Transaction(sender, addressee, amount);
            _accounts.Add(sender);
            _transactions.Add(transaction);
            _transactionProcessor?.ConductTransaction(transaction);
            return transaction;
        }

        public Transaction.Transaction CancelTransaction(Transaction.Transaction transaction)
        {
            if (_transactions.Any(varTransaction => varTransaction.Id == transaction.Id))
            {
                var cancellingTransaction = new Transaction.Transaction(transaction.AddresseeAccount, transaction.SenderAccount, transaction.Amount);
                _transactions.Remove(transaction);
                _transactionProcessor.ConductTransaction(cancellingTransaction);
                return cancellingTransaction;
            }

            throw new BanksExceptions("There is no such Transaction");
        }

        public void ConductCommissionsOneDay()
        {
            foreach (Account account in _accounts)
            {
                account.TimeMachine(1);
            }
        }

        public void AddAccount(Account account)
        {
            if (_accounts.Contains(account) == false)
            {
                _accounts.Add(account);
            }
        }

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void ChangeCreditLimit(decimal creditLimit)
        {
            BankInfo.CreditLimit = creditLimit;
            State = 1;
            foreach (Account account in _accounts)
            {
                account.ChangeLimit(creditLimit);
            }

            Notify();
        }

        public void ChangeCreditCommission(decimal creditCommission)
        {
            BankInfo.CreditLimit = creditCommission;
            State = 2;
            foreach (Account account in _accounts)
            {
                account.ChangeCreditCommission(creditCommission);
            }

            Notify();
        }

        public void ChangeNotReliableTransactions(decimal notReliableTransactions)
        {
            BankInfo.NotReliableTransaction = notReliableTransactions;
            State = 0;
            foreach (Account account in _accounts)
            {
                account.ChangeNotReliableTransactions(notReliableTransactions);
            }

            Notify();
        }
    }
}