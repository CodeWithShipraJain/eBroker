using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.Repository.Implementation
{
    /// <summary>
    /// Trader Equity Repository
    /// </summary>
    public class TraderEquityRepository : ITraderEquityRepository
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
        public TraderEquityRepository(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Function to fetch all the TraderEquity
        /// </summary>
        /// <returns>List of Trader Equity</returns>
        public List<TraderEquity> GetAll()
        {
            return _context.TraderEquities.ToList();
        }

        /// <summary>
        /// Function to fetch the Trader Equity against the trader Id
        /// </summary>
        /// <param name="traderId">Trader Id</param>
        /// <returns>Trader Equity against the trader Id</returns>
        public List<TraderEquity> Get(int traderId)
        {
            return _context.TraderEquities.Where(te => te.TraderId == traderId).ToList();
        }

        /// <summary>
        /// Get the Trader Equity by equity Id
        /// </summary>
        /// <param name="traderId">Trader Id</param>
        /// <param name="equityId">Equity Id</param>
        /// <returns>Trader Equity against the trader Id and Equity Id</returns>
        public TraderEquity Get(int traderId, int equityId)
        {
            return _context.TraderEquities.Where(te => te.TraderId == traderId && te.EquityId == equityId).FirstOrDefault();
        }

        /// <summary>
        /// Function to add or update the trader eqiuity.
        /// </summary>
        /// <param name="TraderEquity">Trader Equity</param>
        /// <returns>Added/Updated Trader Equity</returns>
        public TraderEquity AddOrUpdate(TraderEquity traderEquity)
        {
            var te = _context.TraderEquities
                .Where(te => te.TraderId == traderEquity.TraderId && te.EquityId == traderEquity.EquityId)
                .FirstOrDefault();
            if (te == null)
            {
                te = new TraderEquity
                {
                    TraderId = traderEquity.TraderId,
                    EquityId = traderEquity.EquityId,
                };
                _context.TraderEquities.Add(te);
                _context.SaveChanges();
            }

            te.Quantity = traderEquity.Quantity;
            _context.TraderEquities.Update(te);
            _context.SaveChanges();

            return te;
        }

        #endregion
    }
}
