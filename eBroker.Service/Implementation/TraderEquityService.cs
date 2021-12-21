using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using eBroker.Service.Dto;
using eBroker.Service.Interface;
using eBroker.Service.Utils;
using System;

namespace eBroker.Service.Implementation
{
    /// <summary>
    /// Trader Equity Service
    /// </summary>
    public class TraderEquityService : ITraderEquityService
    {
        #region Properties and Contructor

        /// <summary>
        /// Trade Fund Repository
        /// </summary>
        private ITraderFundRepository _traderFundRepository;

        /// <summary>
        /// Equity Repository
        /// </summary>
        private IEquityRepository _equityRepository;

        /// <summary>
        /// Trade Equity Repository
        /// </summary>
        private ITraderEquityRepository _traderEquityRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="traderFundRepository">Trade Fund Repository</param>
        /// <param name="equityRepository">Equity Repository</param>
        /// <param name="traderEquityRepository">Trade Equity Repository</param>
        public TraderEquityService(ITraderFundRepository traderFundRepository, IEquityRepository equityRepository, ITraderEquityRepository traderEquityRepository)
        {
            _traderFundRepository = traderFundRepository;
            _equityRepository = equityRepository;
            _traderEquityRepository = traderEquityRepository;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Function to buy Equity
        /// </summary>
        /// <param name="equityId">Equity Id</param>
        /// <param name="qty">Quantity</param>
        /// <returns>TraderEquityDto</returns>
        public TradeEquityBuyDto BuyEquity(int equityId, int qty)
        {
            if (!Helper.TimeEligibleForTrading(DateTimeHelper.Now))
            {
                throw new Exception("Time is not eligible for buying equity");
            }

            // fetching trader data
            var trader = _traderFundRepository.GetById(1);

            // fetching equity
            var equity = _equityRepository.GetById(equityId);
            double totalAmount = equity.Price * qty;
            if (trader.RemainingBalance < totalAmount)
            {
                throw new Exception("Insufficient Fund value");
            }

            //fetching trader equity
            var traderEquity = _traderEquityRepository.Get(trader.Id, equityId);
            if (traderEquity == null)
            {
                traderEquity = new TraderEquity { TraderId = trader.Id, EquityId = equity.Id, Quantity = 0 };
            }

            // updating the the database
            traderEquity.Quantity += qty;
            traderEquity = _traderEquityRepository.AddOrUpdate(traderEquity);
            trader.RemainingBalance -= totalAmount;
            _traderFundRepository.Update(trader);

            return new TradeEquityBuyDto(traderEquity, totalAmount, trader.RemainingBalance);
        }

        /// <summary>
        /// Function to sell Equity
        /// </summary>
        /// <param name="equityId">Equity Id</param>
        /// <param name="qty">Quantity</param>
        /// <returns>TraderEquityDto</returns>
        public TraderEquitySellDto SellEquity(int equityId, int qty)
        {
            if (!Helper.TimeEligibleForTrading(DateTimeHelper.Now))
            {
                throw new Exception("Time is not eligible for buying equity");
            }

            // fetching trader data
            var trader = _traderFundRepository.GetById(1);

            //fetching trader equity
            var traderEquity = _traderEquityRepository.Get(trader.Id, equityId);
            if (traderEquity == null || traderEquity.Quantity == 0)
            {
                throw new Exception("Trader don't hold this equity");
            }
            else if (traderEquity.Quantity < qty)
            {
                throw new Exception("Insufficient Equity Quantity");
            }

            // fetching equity
            var equity = _equityRepository.GetById(equityId);
            var totalAmount = equity.Price * qty;
            var brokerage = totalAmount * 0.05 / 100 < 20 ? 20 : totalAmount * 0.05 / 100;

            if (trader.RemainingBalance + (totalAmount - brokerage) < 0)
            {
                throw new Exception("Insufficient Fund value");
            }

            // updating the the database
            traderEquity.Quantity = traderEquity.Quantity - qty;
            traderEquity = _traderEquityRepository.AddOrUpdate(traderEquity);
            trader.RemainingBalance += (totalAmount - brokerage);
            _traderFundRepository.Update(trader);

            return new TraderEquitySellDto(traderEquity, brokerage, trader.RemainingBalance);
        }

        #endregion
    }
}
