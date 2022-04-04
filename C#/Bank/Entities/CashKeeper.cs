namespace Banks.Entities
{
    public class CashKeeper
    {
        public CashKeeper(decimal cash)
        {
            Cash = cash;
            CanUseAccount = true;
            DaysAfterWithdraw = 0;
            DaysOfDeposit = 0;
        }

        public CashKeeper()
        {
            Cash = 0;
            CanUseAccount = true;
            DaysAfterWithdraw = 0;
            DaysOfDeposit = 0;
        }

        public decimal Cash { get; private set; }
        public decimal DaysAfterWithdraw { get; private set; }
        public bool CanUseAccount { get; private set; }
        public decimal DaysOfDeposit { get; private set; }

        public void AddCash(decimal amount)
        {
            Cash += amount;
        }

        public void WithdrawCash(decimal amount)
        {
            Cash -= amount;
        }

        public void SetCanUseAccount(bool canUse)
        {
            CanUseAccount = canUse;
        }

        public void IncrementDaysAfterWithdraw()
        {
            DaysAfterWithdraw += 1;
        }

        public void SetToZeroDaysAfterWithdraw()
        {
            DaysAfterWithdraw = 0;
        }

        public void SetDaysOfDeposit(decimal days)
        {
            DaysOfDeposit = days;
        }
    }
}