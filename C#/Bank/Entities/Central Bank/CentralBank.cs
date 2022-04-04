using System.Collections.Generic;

namespace Banks.Entities.Central_Bank
{
    public class CentralBank
    {
        private List<Bank.Bank> _banks = new List<Bank.Bank>();

        public CentralBank(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void AddBank(Bank.Bank bank)
        {
            _banks.Add(bank);
        }

        public void RewindTime(uint numberOfDays)
        {
            for (int i = 0; i < numberOfDays; i++)
            {
                foreach (Bank.Bank bank in _banks)
                {
                    bank.ConductCommissionsOneDay();
                }
            }
        }
    }
}