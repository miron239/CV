using System.Collections.Generic;
using Banks.Entities.Accounts.DepositBids;

namespace Banks.Entities.Bank
{
    public class BankInfo
    {
        public BankInfo(List<DepositBid> depositBids, decimal creditLimit, decimal creditCommission, decimal notReliableTransaction)
        {
            DepositBids = depositBids;
            CreditLimit = creditLimit;
            CreditCommission = creditCommission;
            NotReliableTransaction = notReliableTransaction;
        }

        public List<DepositBid> DepositBids { get; private set; }
        public decimal CreditLimit { get; internal set; }
        public decimal CreditCommission { get; internal set; }
        public decimal NotReliableTransaction { get; internal set; }
    }
}