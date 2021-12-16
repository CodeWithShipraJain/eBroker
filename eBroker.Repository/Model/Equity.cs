using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eBroker.Repository.Model
{
    /// <summary>
    /// Equity Model
    /// </summary>
    public class Equity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Equity Name
        /// </summary>
        public string EquityName { get; set; }

        /// <summary>
        /// Equity Price
        /// </summary>
        public double Price { get; set; }
    }
}
