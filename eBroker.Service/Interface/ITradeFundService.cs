using eBroker.Service.Dto;


namespace eBroker.Service.Interface
{
    /// <summary>
    /// ITrade Fund Service
    /// </summary>
    public interface ITradeFundService
    {
        /// <summary>
        /// Function to add amount against the trader.
        /// Assuming that there is single trader so amount will be added to that trader
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Updated Trader Fund</returns>
        public TraderFundDto AddFunds(double amount);
    }
}
