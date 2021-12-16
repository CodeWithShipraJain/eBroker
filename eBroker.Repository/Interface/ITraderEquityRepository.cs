using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Repository.Interface
{
    public interface ITraderEquityRepository
    {
        /// <summary>
        /// Function to fetch all the TraderEquity
        /// </summary>
        /// <returns>List of Trader Equity</returns>
        List<TraderEquity> GetAll();

        /// <summary>
        /// Function to fetch the Trader Equity against the trader Id
        /// </summary>
        /// <param name="traderId">Trader Id</param>
        /// <returns>Trader Equity against the trader Id</returns>
        List<TraderEquity> Get(int traderId);

        /// <summary>
        /// Get the Trader Equity by equity Id
        /// </summary>
        /// <param name="traderId">Trader Id</param>
        /// <param name="equityId">Equity Id</param>
        /// <returns>Trader Equity against the trader Id and Equity Id</returns>
        TraderEquity Get(int traderId, int equityId);

        /// <summary>
        /// Function to add or update the trader eqiuity.
        /// </summary>
        /// <param name="TraderEquity">Trader Equity</param>
        /// <returns>Added/Updated Trader Equity</returns>
        TraderEquity AddOrUpdate(TraderEquity traderEquity);
    }
}
