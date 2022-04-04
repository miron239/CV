using System;
using System.Collections.Generic;
using Banks.Entities.Accounts.Account_processors;
using Banks.Entities.Accounts.Account_processors.Add;
using Banks.Entities.Accounts.Account_processors.CommisionProcessor;
using Banks.Entities.Accounts.Account_processors.Withdraw;
using Banks.Entities.Accounts.DepositBids;
using Banks.Entities.Bank;
using Banks.Entities.Client;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class Account
    {
        private CashKeeper _cashKeeper;
        private BankInfo _bankInfo;
        private ClientInfo _clientInfo;
        private IAccountFactory _factory;

        public Account(IAccountFactory factory, BankInfo bankInfo, ClientInfo clientInfo)
        {
            _factory = factory;
            _bankInfo = bankInfo;
            _clientInfo = clientInfo;
            _cashKeeper = new CashKeeper(0);
            Add = factory.CreateAdd();
            Withdraw = factory.CreateWithdraw();
            CommisionProcessor = factory.CreateCommisionProcessor();
        }

        public IAdd Add { get; private set; }
        public IWithdraw Withdraw { get; private set; }
        public ICommisionProcessor CommisionProcessor { get; private set; }
        public IObserver Observer { get; private set; }
        public decimal DaysOfDeposit { get; private set; }
        public bool CanUseAccount { get; private set; }

        public void AddMoney(decimal amount)
        {
            Add.AddMoney(_clientInfo, _bankInfo, _cashKeeper, amount);
        }

        public void WithdrawMoney(decimal amount)
        {
            Withdraw.Withdraw(_clientInfo, _bankInfo, _cashKeeper, amount);
        }

        public void ChangeLimit(decimal creditLimit)
        {
            _bankInfo.CreditLimit = creditLimit;
        }

        public void ChangeNotReliableTransactions(decimal notReliableTransaction)
        {
            _bankInfo.NotReliableTransaction = notReliableTransaction;
        }

        public void ChangeCreditCommission(decimal creditCommission)
        {
            _bankInfo.CreditCommission = creditCommission;
        }

        public decimal CashOnAccount()
        {
            return _cashKeeper.Cash;
        }

        public void SetDaysOfDeposit(decimal days)
        {
            _cashKeeper.SetDaysOfDeposit(days);
        }

        public void TimeMachine(uint days)
        {
            if (_factory.GetType() == typeof(DepositFactory))
            {
                decimal temp = 0, temp2 = 0, t = decimal.MaxValue;

                foreach (DepositBid t1 in _bankInfo.DepositBids)
                {
                    decimal a = Math.Abs(Math.Abs(t1.MinAmount) - Math.Abs(_cashKeeper.Cash));

                    if (a < t)
                    {
                        temp = t1.MinAmount;
                        temp2 = t1.Bid;
                        t = a;
                    }
                }

                for (int i = 0; i < days; i++)
                {
                    CommisionProcessor.ConductCommision(temp2, _cashKeeper);
                }
            }

            if (_factory.GetType() == typeof(DebitFactory))
            {
                for (int i = 0; i < days; i++)
                {
                    CommisionProcessor.ConductCommision(0, _cashKeeper);
                }
            }

            if (_factory.GetType() == typeof(CreditFactory))
            {
                for (int i = 0; i < days; i++)
                {
                    CommisionProcessor.ConductCommision(_bankInfo.CreditCommission, _cashKeeper);
                }
            }
        }

        public void CreateObserver()
        {
            if (_factory.GetType() == typeof(DebitFactory))
            {
                Observer = new DebitObserver();
            }

            if (_factory.GetType() == typeof(CreditFactory))
            {
                Observer = new CreditObserver();
            }

            if (_factory.GetType() == typeof(DepositObserver))
            {
                Observer = new DepositObserver();
            }
        }
    }
}