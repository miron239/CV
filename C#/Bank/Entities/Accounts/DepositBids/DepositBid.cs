namespace Banks.Entities.Accounts.DepositBids
{
    public class DepositBid
    {
        public DepositBid(decimal minAmount, decimal bid)
        {
            MinAmount = minAmount;
            Bid = bid;
        }

        public decimal MinAmount { get; }
        public decimal Bid { get; }
    }
}