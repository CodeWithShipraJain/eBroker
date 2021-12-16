using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Dto
{
    /// <summary>
    /// Trader Equity Dto
    /// </summary>
    public class TraderEquityDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Trader Id
        /// </summary>
        public int TraderId { get; set; }

        /// <summary>
        /// Equity Id
        /// </summary>
        public int EquityId { get; set; }

        /// <summary>
        /// Quantity of Equity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeEquity">Trader Equity</param>
        public TraderEquityDto(TraderEquity tradeEquity)
        {
            Id = tradeEquity.Id;
            TraderId = tradeEquity.TraderId;
            EquityId = tradeEquity.EquityId;
            Quantity = tradeEquity.Quantity;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TraderEquityDto()
        { }
    }
}
