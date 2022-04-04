namespace Banks.Entities.Accounts.Account_processors.CommisionProcessor
{
    public interface ICommisionProcessor
    {
        public void ConductCommision(decimal commission, CashKeeper cashKeeper);
    }
}