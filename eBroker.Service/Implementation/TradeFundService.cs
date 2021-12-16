using eBroker.Repository.Interface;
using eBroker.Service.Dto;
using eBroker.Service.Interface;
using eBroker.Service.Utils;

namespace eBroker.Service.Implementation
{
    /// <summary>
    /// Trade Fund Service
    /// </summary>
    public class TradeFundService : ITradeFundService
    {
        #region Properties and Contructor

        /// <summary>
        /// Trade Fund Repository
        /// </summary>
        private ITraderFundRepository _traderFundRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="traderFundRepository">Trade Fund Repository</param>
        public TradeFundService(ITraderFundRepository traderFundRepository)
        {
            _traderFundRepository = traderFundRepository;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Function to add the fund value
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <returns>Updated Trader Fund</returns>
        public TraderFundDto AddFunds(double amount)
        {
            // calculating the total amount to be added after deducting the fund charge
            amount = amount - Helper.CalculateAddFundCharge(amount);

            // updating the thebalance
            var fund = _traderFundRepository.GetById(1);
            fund.RemainingBalance += amount;
            _traderFundRepository.Update(fund);

            return new TraderFundDto(fund);
        }

        #endregion
    }
}
