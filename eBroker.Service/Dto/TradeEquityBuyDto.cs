using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Dto
{
    /// <summary>
    /// Trader Equity Buy Dto
    /// </summary>
    public class TradeEquityBuyDto : TraderEquityDto
    {
        /// <summary>
        /// Total cost that trader incurred while buying equity
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// Remainign trader balance after buying equity
        /// </summary>
        public double RemainingTraderBalance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeEquity">Trader Equity</param>
        public TradeEquityBuyDto(TraderEquity tradeEquity, double totalCost, double remainingBalance):base(tradeEquity)
        {
            this.TotalCost = totalCost;
            this.RemainingTraderBalance = remainingBalance;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TradeEquityBuyDto()
        { }
    }
}
