using System;
using Banks.Entities.Bank;

namespace Banks.Entities.Client
{
    public class DepositObserver : IObserver
    {
        public void Update(ISubject subject)
        {
            {
                    if (((Bank.Bank)subject).State is > 5 or 0)
                    {
                        Console.WriteLine("Debit: Reacted to the event.");
                    }
            }
        }
    }
}