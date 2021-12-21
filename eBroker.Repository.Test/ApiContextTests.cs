using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace eBroker.Repository.Test
{
    /// <summary>
    /// Api Context Test Class
    /// </summary>
    public class ApiContextTests
    {
        #region Properties and Contructor

        /// <summary>
        /// DbContext Options
        /// </summary>
        DbContextOptions options;

        /// <summary>
        /// Contructor
        /// </summary>
        public ApiContextTests()
        {
            options = Utility.CreateNewContextOptions();
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Method to test if data is loaded correctly.
        /// </summary>
        [Fact]
        public void LoadData_AllInitalDataLoaded()
        {
            // Arrange
            using (var context = new ApiContext(options))
            {
                // Act
                context.LoadData();

                // Assert
                Assert.Equal(7, context.Equities.Count());
                Assert.Equal(1, context.TraderFunds.Count());
            }
        }

        #endregion
    }
}
