using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Repository.Interface
{
    /// <summary>
    /// Equity Repository Interface
    /// </summary>
    public interface IEquityRepository
    {
        /// <summary>
        /// Function to fetch all the Equities
        /// </summary>
        /// <returns>List of Equities</returns>
        List<Equity> GetAll();

        /// <summary>
        /// Function to fetch the Equity against the particular Id
        /// </summary>
        /// <param name="id">Equity Id</param>
        /// <returns>Equity against the Id</returns>
        Equity GetById(int id);
    }
}
