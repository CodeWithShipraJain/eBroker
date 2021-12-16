using eBroker.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Repository
{
    /// <summary>
    /// Api Context
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <summary>
        /// DbSet for Equity
        /// </summary>
        public DbSet<Equity> Equities { get; set; }

        /// <summary>
        /// DbSet for TraderEquity
        /// </summary>
        public DbSet<TraderEquity> TraderEquities { get; set; }

        /// <summary>
        /// DbSet for TraderFund
        /// </summary>
        public DbSet<TraderFund> TraderFunds { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DbContext Options</param>
        public ApiContext(DbContextOptions options) : base(options)
        {
        }
    }
}
