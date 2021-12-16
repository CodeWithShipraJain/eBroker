using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Utils
{
    /// <summary>
    /// Helper Utility Function
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Function to calculate the Charge on the amount being added tothe Fund
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <returns>Charge</returns>
        public static double CalculateAddFundCharge(double amount)
        {
            double charge = 0;

            if (amount > 100000)
            {
                charge = 0.05 * amount / 100;
            }

            return charge;
        }

        /// <summary>
        /// Function to check if current time is eligible for trading.
        /// </summary>
        /// <returns>True if it is eligible for trading; ow false</returns>
        public static bool TimeEligibleForTrading(DateTime time)
        {
            bool isEligible = false;

            if (time.DayOfWeek != DayOfWeek.Saturday && time.DayOfWeek != DayOfWeek.Sunday && time.TimeOfDay >= new TimeSpan(9, 0, 0) && time.TimeOfDay < new TimeSpan(15, 0, 0))
            {
                isEligible = true;
            }
            return isEligible;
        }
    }
}
