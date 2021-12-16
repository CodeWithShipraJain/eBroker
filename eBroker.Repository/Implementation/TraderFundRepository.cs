using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.Repository.Implementation
{
    /// <summary>
    /// Trader Fund Repository
    /// </summary>
    public class TraderFundRepository : ITraderFundRepository
    {
        #region Properties and Contructor

        /// <summary>
        /// Api context
        /// </summary>
        private ApiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Api Context</param>
        public TraderFundRepository(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Function to fetch all the TraderFund
        /// </summary>
        /// <returns>List of Trader Funds</returns>
        public List<TraderFund> GetAll()
        {
            return _context.TraderFunds.ToList();
        }

        /// <summary>
        /// Function to fetch the Trader Fund against the particular Id
        /// </summary>
        /// <param name="id">Trader Fund Id</param>
        /// <returns>Trader Fund against the Id</returns>
        public TraderFund GetById(int id)
        {
            return _context.TraderFunds.Find(id);
        }

        /// <summary>
        /// Update the trader fund.
        /// </summary>
        /// <param name="traderFund">Trader Fund</param>
        public void Update(TraderFund traderFund)
        {
            var dbTraderFund = _context.TraderFunds.FirstOrDefault(i => i.Id == traderFund.Id);

            if (dbTraderFund == null)
                throw new Exception("Trader Fund not found");

            dbTraderFund.RemainingBalance = traderFund.RemainingBalance;
            _context.TraderFunds.Update(dbTraderFund);
            _context.SaveChanges();
        }

        #endregion
    }
}
