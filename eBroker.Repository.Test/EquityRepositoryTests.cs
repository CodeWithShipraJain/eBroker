using eBroker.Repository.Implementation;
using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eBroker.Repository.Test
{
    // [MethodUnderTest]_[Scenario]_[ExpectedResult]

    /// <summary>
    /// Equity Repository Test Class
    /// </summary>
    public class EquityRepositoryTests
    {
        #region Properties and Contructor

        /// <summary>
        /// DbContext Options
        /// </summary>
        DbContextOptions options;

        /// <summary>
        /// Contructor
        /// </summary>
        public EquityRepositoryTests()
        {
            options = Utility.CreateNewContextOptions();

            // Generating Data
            using (var context = new ApiContext(options))
            {
                context.Equities.Add(new Equity { Id = 1, EquityName = "HIL", Price = 42.11 });
                context.Equities.Add(new Equity { Id = 2, EquityName = "ITC", Price = 202.43 });
                context.Equities.Add(new Equity { Id = 3, EquityName = "TCS", Price = 321.21 });
                context.SaveChanges();
            }
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Method to test if data is present in the Equity Table.
        /// </summary>
        [Fact]
        public void GetAll_FetchData_DataIsPresent()
        {
            using (var context = new ApiContext(options))
            {
                IEquityRepository equityRepo = new EquityRepository(context);

                var equityList = equityRepo.GetAll();

                Assert.NotNull(equityList);
            }
        }

        /// <summary>
        /// Method it test that the Equity with id 1 exist; and it has name and its price is non-negative.
        /// </summary>
        [Fact]
        public void GetById_FetchData_DataIsPresent()
        {
            using (var context = new ApiContext(options))
            {
                IEquityRepository equityRepo = new EquityRepository(context);

                var equity = equityRepo.GetById(1);

                Assert.NotNull(equity);
                Assert.NotEmpty(equity.EquityName);
                Assert.True(equity.Price >= 0);
            }
        }
        
        /// <summary>
        /// Test method to verify that equity data donot exist for non-existing id
        /// </summary>
        [Fact]
        public void GetById_FetchData_DataIsNotPresent()
        {
            using (var context = new ApiContext(options))
            {
                IEquityRepository equityRepo = new EquityRepository(context);

                var equity = equityRepo.GetById(11);

                Assert.Null(equity);
            }
        }

        #endregion
    }
}
