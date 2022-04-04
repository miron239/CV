namespace Banks.Entities.Accounts.Account_processors.CommisionProcessor
{
    public class DepositCommisionStrategy : ICommisionProcessor
    {
        public void ConductCommision(decimal commission, CashKeeper cashKeeper)
        {
            cashKeeper.IncrementDaysAfterWithdraw();
            if (cashKeeper.DaysAfterWithdraw == cashKeeper.DaysOfDeposit)
            {
                cashKeeper.SetCanUseAccount(true);
                cashKeeper?.AddCash(cashKeeper.Cash * commission);
                cashKeeper.SetToZeroDaysAfterWithdraw();
            }
        }
    }
}