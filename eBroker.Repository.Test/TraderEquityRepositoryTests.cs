using eBroker.Repository.Implementation;
using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace eBroker.Repository.Test
{
    /// <summary>
    /// Trader Equity Repository Test Class
    /// </summary>
    public class TraderEquityRepositoryTests
    {
        #region Properties and Contructor

        /// <summary>
        /// DbContext Options
        /// </summary>
        DbContextOptions options;

        /// <summary>
        /// Contructor
        /// </summary>
        public TraderEquityRepositoryTests()
        {
            options = Utility.CreateNewContextOptions();

            // Generating Data
            using (var context = new ApiContext(options))
            {
                context.Equities.Add(new Equity { Id = 1, EquityName = "HIL", Price = 42.11 });
                context.Equities.Add(new Equity { Id = 2, EquityName = "ITC", Price = 202.43 });
                context.Equities.Add(new Equity { Id = 3, EquityName = "TCS", Price = 321.21 });

                context.TraderFunds.Add(new TraderFund { Id = 1, RemainingBalance = 0 });
                context.SaveChanges();
            }
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Method to test if data is absent in the Trader Equity Table.
        /// </summary>
        [Fact]
        public void GetAll_FetchData_DataIsAbsent()
        {
            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);

                var equityList = traderEquityRepo.GetAll();

                Assert.Empty(equityList);
            }
        }


        /// <summary>
        /// Method to test if data is present in the Trader Equity Table.
        /// </summary>
        [Fact]
        public void GetAll_FetchData_DataIsPresent()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);

                var traderEquityList = traderEquityRepo.GetAll();

                Assert.NotEmpty(traderEquityList);
                Assert.True(traderEquityList.Count == 2);
            }
        }

        /// <summary>
        /// Method it test that the Trader Equity table contains data for the Traders id 1.
        /// </summary>
        [Fact]
        public void GetByTraderId_FetchData_DataIsPresent()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            using (var context = new ApiContext(options))
            {
                // act
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquityList = traderEquityRepo.Get(1);

                //assert
                Assert.NotEmpty(traderEquityList);
                Assert.True(traderEquityList.Count == 2);
            }
        }

        /// <summary>
        /// Method it test that the Trader Equity table contains data for the Traders id other than 1.
        /// </summary>
        [Fact]
        public void GetByTraderId_FetchData_DataIsNotPresent()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            using (var context = new ApiContext(options))
            {
                // act
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquityList = traderEquityRepo.Get(2);

                //assert
                Assert.Empty(traderEquityList);
            }
        }

        /// <summary>
        /// Test method to verify that trader equity data exist for given trader id and equity id
        /// </summary>
        [Fact]
        public void GetByTraderIdEquityId_FetchData_DataIsPresent()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            using (var context = new ApiContext(options))
            {
                // act
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquity = traderEquityRepo.Get(1, 1);

                //assert
                Assert.NotNull(traderEquity);
            }
        }

        /// <summary>
        /// Test method to verify that trader equity data doesnot exist for given trader id and equity id
        /// </summary>
        [Fact]
        public void GetByTraderIdEquityId_FetchData_DataIsNotPresent()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            using (var context = new ApiContext(options))
            {
                // act
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquity = traderEquityRepo.Get(2, 1);

                //assert
                Assert.Null(traderEquity);
            }
        }

        /// <summary>
        /// Function to verify that the record is added
        /// </summary>
        [Fact]
        public void AddOrUpdate_AddNewRecord_AddedSuccessfully()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            TraderEquity addedRec;
            // act
            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                addedRec = traderEquityRepo.AddOrUpdate(new TraderEquity { TraderId = 1, EquityId = 3, Quantity = 1 });

                Assert.NotNull(addedRec);
            }

            // assert
            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquity = traderEquityRepo.Get(1, 3);

                //assert
                Assert.NotNull(traderEquity);
                Assert.Equal(addedRec.Id, traderEquity.Id);
                Assert.Equal(addedRec.TraderId, traderEquity.TraderId);
                Assert.Equal(addedRec.EquityId, traderEquity.EquityId);
                Assert.Equal(addedRec.Quantity, traderEquity.Quantity);
            }
        }

        /// <summary>
        /// Function to verify that the record is Updated
        /// </summary>
        [Fact]
        public void AddOrUpdate_UpdateRecord_UpdatedSuccessfully()
        {
            // arrange
            using (var context = new ApiContext(options))
            {
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 1 });
                context.TraderEquities.Add(new TraderEquity { TraderId = 1, EquityId = 2, Quantity = 1 });
                context.SaveChanges();
            }

            TraderEquity updatedRec;
            // act
            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                updatedRec = traderEquityRepo.AddOrUpdate(new TraderEquity { TraderId = 1, EquityId = 1, Quantity = 4 });

                Assert.NotNull(updatedRec);
            }

            // assert
            using (var context = new ApiContext(options))
            {
                ITraderEquityRepository traderEquityRepo = new TraderEquityRepository(context);
                var traderEquity = traderEquityRepo.Get(1, 1);

                //assert
                Assert.NotNull(traderEquity);
                Assert.Equal(updatedRec.Id, traderEquity.Id);
                Assert.Equal(updatedRec.TraderId, traderEquity.TraderId);
                Assert.Equal(updatedRec.EquityId, traderEquity.EquityId);
                Assert.Equal(updatedRec.Quantity, traderEquity.Quantity);
            }
        }

        #endregion
    }
}
