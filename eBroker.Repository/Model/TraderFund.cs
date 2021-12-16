using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eBroker.Repository.Model
{
    /// <summary>
    /// Trader Fund Model
    /// </summary>
    public class TraderFund
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Remaining Balance
        /// </summary>
        public double RemainingBalance { get; set; }
    }
}
