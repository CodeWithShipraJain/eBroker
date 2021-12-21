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

        /// <summary>
        /// Function to load the inital data
        /// </summary>
        public void LoadData()
        {
            Equities.Add(new Equity { Id = 1, EquityName = "HIL", Price = 42.11 });
            Equities.Add(new Equity { Id = 2, EquityName = "ITC", Price = 202.43 });
            Equities.Add(new Equity { Id = 3, EquityName = "TCS", Price = 321.21 });
            Equities.Add(new Equity { Id = 4, EquityName = "India Bulls", Price = 1020.21 });
            Equities.Add(new Equity { Id = 5, EquityName = "HDFC Bank", Price = 1522.35 });
            Equities.Add(new Equity { Id = 6, EquityName = "PNB", Price = 40.75 });
            Equities.Add(new Equity { Id = 7, EquityName = "Reliance", Price = 2500.54 });

            TraderFunds.Add(new TraderFund { Id = 1, RemainingBalance = 0 });

            SaveChanges();
        }
    }
}
