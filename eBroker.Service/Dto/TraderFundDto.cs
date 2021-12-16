using eBroker.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Dto
{
    public class TraderFundDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Remaining Balance
        /// </summary>
        public double RemainingBalance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeFund">Trader Fund</param>
        public TraderFundDto(TraderFund tradeFund)
        {
            Id = tradeFund.Id;
            RemainingBalance = tradeFund.RemainingBalance;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TraderFundDto()
        { }
    }
}
