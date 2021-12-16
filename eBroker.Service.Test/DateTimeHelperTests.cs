using eBroker.Service.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Service.Test
{
    public class DateTimeHelperTests
    {
        /// <summary>
        /// Function to test that getting Now Date return current date if the value is not set
        /// </summary>
        [Fact]
        public void GetNow_WithoutSetting_CurrentDate()
        {
            // Arrange
            var currentDate = DateTime.Now;

            // Act
            var helperNowDate = DateTimeHelper.Now;
            var difference = (helperNowDate - currentDate).TotalSeconds;

            // Assert
            Assert.True(helperNowDate != DateTime.MinValue);
            Assert.True(difference >= 0);
        }

        /// <summary>
        /// Function to test that getting Now Date return assigned date
        /// </summary>
        [Fact]
        public void GetNow_WithSetting_AssignedDate()
        {
            // Arrange
            var assignedDate = new DateTime(2021, 12, 12, 13, 15, 43);

            // Act
            DateTimeHelper.Set(assignedDate);
            var helperNowDate = DateTimeHelper.Now;
            var difference = (assignedDate - helperNowDate).TotalSeconds;

            // Assert
            Assert.True(helperNowDate != DateTime.MinValue);
            Assert.True(helperNowDate.Year == 2021);
            Assert.True(helperNowDate.Month == 12);
            Assert.True(helperNowDate.Day == 12);
            Assert.True(helperNowDate.Hour == 13);
            Assert.True(helperNowDate.Minute == 15);
            Assert.True(helperNowDate.Second == 43);
            Assert.True(difference == 0);
        }

        /// <summary>
        /// Function to test that getting Now Date return assigned date
        /// </summary>
        [Fact]
        public void GetNow_AfterReset_CurrentDate()
        {
            // Arrange
            var assignedDate = new DateTime(2021, 12, 12, 13, 15, 43);
            DateTimeHelper.Set(assignedDate);
            var currentDate = DateTime.Now;

            // Act
            DateTimeHelper.Reset();
            var helperNowDate = DateTimeHelper.Now;
            var difference = (helperNowDate - currentDate).TotalSeconds;

            // Assert
            Assert.True(helperNowDate != DateTime.MinValue);
            Assert.True(difference >= 0);
        }
    }
}
