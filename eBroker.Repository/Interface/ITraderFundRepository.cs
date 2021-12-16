using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Repository.Interface
{
    public interface ITraderFundRepository
    {
        /// <summary>
        /// Function to fetch all the TraderFund
        /// </summary>
        /// <returns>List of Trader Funds</returns>
        List<TraderFund> GetAll();

        /// <summary>
        /// Function to fetch the Trader Fund against the particular Id
        /// </summary>
        /// <param name="id">Trader Fund Id</param>
        /// <returns>Trader Fund against the Id</returns>
        TraderFund GetById(int id);

        /// <summary>
        /// Update the trader fund.
        /// </summary>
        /// <param name="traderFund">Trader Fund</param>
        void Update(TraderFund traderFund);
    }
}
