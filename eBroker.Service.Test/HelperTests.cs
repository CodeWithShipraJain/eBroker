using eBroker.Service.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Service.Test
{
    public class HelperTests
    {
        /// <summary>
        /// Function to test that amount is added to the fund balance if amount added is less than 1 l
        /// </summary>
        [Fact]
        public void CalculateAddFundCharge_AmountLessThan1Lac()
        {
            // Arrange
            double amount = 50000;

            // Act
            var charge = Helper.CalculateAddFundCharge(amount);

            // Assert
            Assert.Equal(0, charge);
        }

        /// <summary>
        /// Function to test that amount is added to the fund balance if amount added is more than 1 l
        /// </summary>
        [Fact]
        public void CalculateAddFundCharge_AmountMoreThan1Lac()
        {
            // Arrange
            double amount = 150000;

            // Act
            var charge = Helper.CalculateAddFundCharge(amount);

            // Assert
            Assert.Equal(0.05 * amount / 100, charge);
        }

        /// <summary>
        /// Function to test that the dae time provided is eligible for trading
        /// Provided date is weekend
        /// </summary>
        [Fact]
        public void TimeEligibleForTrading_Weekend_NotEligible()
        {
            // Arrange
            DateTime time = new DateTime(2021, 12, 11, 15, 13, 15);

            // Act
            var isEligible = Helper.TimeEligibleForTrading(time);

            // Assert
            Assert.False(isEligible);
        }

        /// <summary>
        /// Function to test that the dae time provided is eligible for trading
        /// Provided date is weekday in between 9Am to 3PM
        /// </summary>
        [Fact]
        public void TimeEligibleForTrading_WeekDayBetween9AMTo3PM_Eligible()
        {
            // Arrange
            DateTime time = new DateTime(2021, 12, 8, 10, 13, 15);

            // Act
            var isEligible = Helper.TimeEligibleForTrading(time);

            // Assert
            Assert.True(isEligible);
        }

        /// <summary>
        /// Function to test that the dae time provided is eligible for trading
        /// Provided date is weekday not in between 9Am to 3PM
        /// </summary>
        [Fact]
        public void TimeEligibleForTrading_WeekDayNotBetween9AMTo3PM_Eligible()
        {
            // Arrange
            DateTime time = new DateTime(2021, 12, 8, 19, 13, 15);

            // Act
            var isEligible = Helper.TimeEligibleForTrading(time);

            // Assert
            Assert.False(isEligible);
        }
    }
}
