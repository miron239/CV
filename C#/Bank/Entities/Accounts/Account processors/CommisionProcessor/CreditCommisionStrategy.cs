namespace Banks.Entities.Accounts.Account_processors.CommisionProcessor
{
    public class CreditCommisionStrategy : ICommisionProcessor
    {
        public void ConductCommision(decimal commission, CashKeeper cashKeeper)
        {
            cashKeeper.IncrementDaysAfterWithdraw();
            if (cashKeeper.Cash < 0)
            {
                cashKeeper.WithdrawCash(-1 * cashKeeper.Cash * commission);
            }
        }
    }
}