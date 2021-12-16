using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.Repository.Implementation
{
    /// <summary>
    /// Equity Repository
    /// </summary>
    public class EquityRepository : IEquityRepository
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
        public EquityRepository(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Function to fetch all the Equities
        /// </summary>
        /// <returns>List of Equities</returns>
        public List<Equity> GetAll()
        {
            return _context.Equities.ToList();
        }

        /// <summary>
        /// Function to fetch the Equity against the particular Id
        /// </summary>
        /// <param name="id">Equity Id</param>
        /// <returns>Equity against the Id</returns>
        public Equity GetById(int id)
        {
            return _context.Equities.Find(id);
        }

        #endregion
    }
}
