using eBroker.Repository.Implementation;
using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace eBroker.Repository.Test
{
    /// <summary>
    /// TraderFund Repository Test Class
    /// </summary>
    public class TraderFundRepositoryTests
    {
        #region Properties and Contructor

        /// <summary>
        /// DbContext Options
        /// </summary>
        DbContextOptions options;

        /// <summary>
        /// Contructor
        /// </summary>
        public TraderFundRepositoryTests()
        {
            options = Utility.CreateNewContextOptions();

            // Generating Data
            using (var context = new ApiContext(options))
            {
                context.TraderFunds.Add(new TraderFund { Id = 1, RemainingBalance = 0 });
                context.SaveChanges();
            }
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Method to test if data is present in the Trader Fund Table.
        /// </summary>
        [Fact]
        public void GetAll_FetchData_DataIsPresent()
        {
            using (var context = new ApiContext(options))
            {
                ITraderFundRepository traderFundRepo = new TraderFundRepository(context);

                var equityList = traderFundRepo.GetAll();

                Assert.NotNull(equityList);
            }
        }

        /// <summary>
        /// Method it test that the trader Fund with id 1 exist; and its RemainingBalance is zero.
        /// </summary>
        [Fact]
        public void GetById_FetchDataForId1_DataIsPresent()
        {
            using (var context = new ApiContext(options))
            {
                ITraderFundRepository traderFundRepo = new TraderFundRepository(context);

                var traderFund = traderFundRepo.GetById(1);

                Assert.NotNull(traderFund);
                Assert.True(traderFund.RemainingBalance == 0);
            }
        }

        /// <summary>
        /// Test method to verify that trader Fund with id that is not one and data donot exist for non-existing id that is 1
        /// </summary>
        [Fact]
        public void GetById_FetchDataForIdOtherThan1_DataIsNotPresent()
        {
            using (var context = new ApiContext(options))
            {
                ITraderFundRepository traderFundRepo = new TraderFundRepository(context);

                var traderFund = traderFundRepo.GetById(2);

                Assert.Null(traderFund);
            }
        }

        /// <summary>
        /// Test method to verify that trader Fund with id 1 is getting updated
        /// </summary>
        [Fact]
        public void Update_UpdateForId1_Success()
        {
            using (var context = new ApiContext(options))
            {
                ITraderFundRepository traderFundRepo = new TraderFundRepository(context);

                // act
                traderFundRepo.Update(new TraderFund { Id = 1, RemainingBalance = 20});

                // assert
                var updatedTraderFund = traderFundRepo.GetById(1);
                Assert.NotNull(updatedTraderFund);
                Assert.True(updatedTraderFund.RemainingBalance == 20);
            }
        }

        /// <summary>
        /// Test method to verify that trader Fund with id 1 is getting updated
        /// </summary>
        [Fact]
        public void Update_UpdateForIdOtherThan1_ThrowException()
        {
            using (var context = new ApiContext(options))
            {
                ITraderFundRepository traderFundRepo = new TraderFundRepository(context);

                // act and arrange
                Exception ex = Assert.Throws<Exception>(() => traderFundRepo.Update(new TraderFund { Id = 2, RemainingBalance = 20 }));
                Assert.Equal("Trader Fund not found", ex.Message);
            }
        }

        #endregion
    }
}
