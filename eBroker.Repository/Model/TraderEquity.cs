using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBroker.Repository.Model
{
    /// <summary>
    /// Trader Equity
    /// </summary>
    public class TraderEquity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }
}
