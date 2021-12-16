using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Service.Utils
{
    /// <summary>
    /// Date Time Class
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// Date time set externally
        /// </summary>
        private static DateTime? dateTime;

        /// <summary>
        /// Get Date Time
        /// </summary>
        public static DateTime Now { get { return dateTime ?? DateTime.Now; } }

        /// <summary>
        /// Function to set the date time
        /// </summary>
        /// <param name="setDateTime">DateTime</param>
        public static void Set(DateTime setDateTime)
        {
            dateTime = setDateTime;
        }

        /// <summary>
        /// Function to reset the date time
        /// </summary>
        public static void Reset()
        {
            dateTime = null;
        }
    }
}
