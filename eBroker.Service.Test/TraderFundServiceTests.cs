using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using eBroker.Service.Implementation;
using Moq;
using Xunit;

namespace eBroker.Service.Test
{
    /// <summary>
    /// Trader Fund Service Tests
    /// </summary>
    public class TraderFundServiceTests
    {
        #region Properties and Contructor

        /// <summary>
        /// Trader Fund Moq Repository
        /// </summary>
        Mock<ITraderFundRepository> traderFundRepositoryMoq;

        /// <summary>
        /// Contructor
        /// </summary>
        public TraderFundServiceTests()
        {
            TraderFund traderFund = new TraderFund { Id = 1, RemainingBalance = 50000 };
            traderFundRepositoryMoq = new Mock<ITraderFundRepository>();
            traderFundRepositoryMoq.Setup(repo => repo.GetById(traderFund.Id)).Returns(traderFund);
            traderFundRepositoryMoq.Setup(repo => repo.Update(It.IsAny<TraderFund>())).Verifiable();
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Function to test that amount is added to the fund balance if amount added is less than 1 l
        /// </summary>
        [Fact]
        public void AddFunds_AddAmountLessThanOneLac_AmountUpdated()
        {
            // Arrange
            double amount = 50000;            
            var traderFundService = new TradeFundService(traderFundRepositoryMoq.Object);

            // Act
            var updatedFund = traderFundService.AddFunds(amount);

            // Assert
            Assert.NotNull(updatedFund);
            Assert.Equal(1, updatedFund.Id);
            Assert.Equal(100000, updatedFund.RemainingBalance);
        }

        /// <summary>
        /// Function to test that amount is added to the fund balance if amount added is more than 1 l
        /// </summary>
        [Fact]
        public void AddFunds_AddAmountMoreThanOneLac_AmountUpdated()
        {
            // Arrange
            double amount = 150000;
            var traderFundService = new TradeFundService(traderFundRepositoryMoq.Object);

            // Act
            var updatedFund = traderFundService.AddFunds(amount);

            // Assert
            Assert.NotNull(updatedFund);
            Assert.Equal(1, updatedFund.Id);
            Assert.Equal(50000 + 150000 - (150000 * 0.05 / 100), updatedFund.RemainingBalance);
        }

        #endregion
    }
}
