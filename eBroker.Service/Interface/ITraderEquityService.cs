using eBroker.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Interface
{
    public interface ITraderEquityService
    {
        /// <summary>
        /// Function to buy Equity
        /// </summary>
        /// <param name="equityId">Equity Id</param>
        /// <param name="qty">Quantity</param>
        /// <returns>TraderEquityBuyDto</returns>
        TradeEquityBuyDto BuyEquity(int equityId, int qty);

        /// <summary>
        /// Function to sell Equity
        /// </summary>
        /// <param name="equityId">Equity Id</param>
        /// <param name="qty">Quantity</param>
        /// <returns>TraderEquitySellDto</returns>
        TraderEquitySellDto SellEquity(int equityId, int qty);
    }
}
