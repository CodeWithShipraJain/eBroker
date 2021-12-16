using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Dto
{
    /// <summary>
    /// Trader Equity Sell Dto
    /// </summary>
    public class TraderEquitySellDto : TraderEquityDto
    {
        /// <summary>
        /// Total brokerage cost that trader incurred while selling equity
        /// </summary>
        public double TotalBrokerage { get; set; }

        /// <summary>
        /// Remainign trader balance after selling equity
        /// </summary>
        public double RemainingTraderBalance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeEquity">Trader Equity</param>
        public TraderEquitySellDto(TraderEquity tradeEquity, double totalBrokerage, double remainingBalance) : base(tradeEquity)
        {
            this.TotalBrokerage = totalBrokerage;
            this.RemainingTraderBalance = remainingBalance;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TraderEquitySellDto()
        { }
    }
}
