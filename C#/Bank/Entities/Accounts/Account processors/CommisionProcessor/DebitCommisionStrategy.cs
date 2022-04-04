namespace Banks.Entities.Accounts.Account_processors.CommisionProcessor
{
    public class DebitCommisionStrategy : ICommisionProcessor
    {
        public void ConductCommision(decimal commission, CashKeeper cashKeeper)
        {
            cashKeeper.IncrementDaysAfterWithdraw();
        }
    }
}