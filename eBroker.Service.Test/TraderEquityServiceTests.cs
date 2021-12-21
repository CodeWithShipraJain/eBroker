using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using eBroker.Service.Implementation;
using eBroker.Service.Utils;
using Moq;
using System;
using Xunit;

namespace eBroker.Service.Test
{
    /// <summary>
    /// Trader Equity Service Tests
    /// </summary>
    public class TraderEquityServiceTests
    {
        #region Properties and Contructor

        /// <summary>
        /// Trader Fund Moq Repository
        /// </summary>
        private readonly Mock<ITraderFundRepository> _traderFundRepositoryMoq;

        /// <summary>
        /// Equity Moq Repository
        /// </summary>
        private readonly Mock<IEquityRepository> _equityRepositoryMoq;

        /// <summary>
        /// Trader Equity Moq Repository
        /// </summary>
        private readonly Mock<ITraderEquityRepository> _traderEquityRepositoryMoq;

        /// <summary>
        /// Entity to hold the trader fund data
        /// </summary>
        private readonly TraderFund _traderFund;

        /// <summary>
        /// Entity to hold the equity data with id 1
        /// </summary>
        private readonly Equity _equity1;

        /// <summary>
        /// Entity to hold the equity data with id 1
        /// </summary>
        private readonly Equity _equity2;

        /// <summary>
        /// Entity to hold the equity data with id 4
        /// </summary>
        private readonly Equity _equity4;

        /// <summary>
        /// Entity to hold the trader equity data with equity id 1
        /// </summary>
        private readonly TraderEquity _traderEquity1;

        /// <summary>
        /// Entity to hold the trader equity data with equity id 3
        /// </summary>
        private readonly TraderEquity _traderEquity3;

        /// <summary>
        /// Entity to hold the trader equity data with equity id 4
        /// </summary>
        private readonly TraderEquity _traderEquity4;

        /// <summary>
        /// Contructor
        /// </summary>
        public TraderEquityServiceTests()
        {
            _traderFund = new TraderFund { Id = 1, RemainingBalance = 50000 };
            _equity1 = new Equity { Id = 1, EquityName = "Testing", Price = 100 };
            _equity2 = new Equity { Id = 2, EquityName = "Testing2", Price = 200 };
            _equity4 = new Equity { Id = 4, EquityName = "Testing4", Price = 1500 };
            _traderEquity1 = new TraderEquity { Id = 1, TraderId = 1, EquityId = 1, Quantity = 100 };
            _traderEquity3 = new TraderEquity { Id = 2, TraderId = 1, EquityId = 3, Quantity = 0 };
            _traderEquity4 = new TraderEquity { Id = 3, TraderId = 1, EquityId = 4, Quantity = 10000 };

            _traderFundRepositoryMoq = new Mock<ITraderFundRepository>();
            _traderFundRepositoryMoq.Setup(repo => repo.GetById(_traderFund.Id)).Returns(_traderFund);
            _traderFundRepositoryMoq.Setup(repo => repo.Update(It.IsAny<TraderFund>())).Verifiable();


            _equityRepositoryMoq = new Mock<IEquityRepository>();
            _equityRepositoryMoq.Setup(repo => repo.GetById(_equity1.Id)).Returns(_equity1);
            _equityRepositoryMoq.Setup(repo => repo.GetById(_equity2.Id)).Returns(_equity2);
            _equityRepositoryMoq.Setup(repo => repo.GetById(_equity4.Id)).Returns(_equity4);

            _traderEquityRepositoryMoq = new Mock<ITraderEquityRepository>();
            _traderEquityRepositoryMoq.Setup(repo => repo.Get(_traderFund.Id, _equity1.Id)).Returns(_traderEquity1);
            _traderEquityRepositoryMoq.Setup(repo => repo.Get(_traderFund.Id, _equity2.Id)).Returns((TraderEquity)null);
            _traderEquityRepositoryMoq.Setup(repo => repo.Get(_traderFund.Id, 3)).Returns(_traderEquity3);
            _traderEquityRepositoryMoq.Setup(repo => repo.Get(_traderFund.Id, _equity4.Id)).Returns(_traderEquity4);
            _traderEquityRepositoryMoq.Setup(repo => repo.AddOrUpdate(It.IsAny<TraderEquity>())).Returns<TraderEquity>(x=>x);
        }

        #endregion

        #region Test Methods for BuyEquity

        /// <summary>
        /// Function to test buy equity at non trading hours
        /// </summary>
        [Fact]
        public void BuyEquity_BuyingAtNonTradingHour_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 11, 15, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.BuyEquity(1, 1));
            Assert.Equal("Time is not eligible for buying equity", ex.Message);
        }

        /// <summary>
        /// Function to test buy equity when trader has insufficient fund value
        /// </summary>
        [Fact]
        public void BuyEquity_BuyingAtInSufficientFundValue_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 1, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            // As total cost of buying equity will be 100000 and trader has fund limit of 50000
            Exception ex = Assert.Throws<Exception>(() => traderFundService.BuyEquity(1, 1000));
            Assert.Equal("Insufficient Fund value", ex.Message);
        }

        /// <summary>
        /// Function to test buy equity when trader already has some quantity of that equity
        /// </summary>
        [Fact]
        public void BuyEquity_BuyingEquityThatAlreadyExist_Success()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 1, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);
            int equityId = 1;
            int qty = 100;
            int expectedQty = qty + _traderEquity1.Quantity;
            double expectedTraderBalance = _traderFund.RemainingBalance - (qty * _equity1.Price);

            // Act
            // As per arrange the trader has 100 quantity of the equity with id 1
            var traderEquity = traderFundService.BuyEquity(equityId, qty);

            // Assert
            Assert.NotNull(traderEquity);
            Assert.Equal(expectedQty, traderEquity.Quantity);
            Assert.Equal(_equity1.Price * qty, traderEquity.TotalCost);
            Assert.Equal(expectedTraderBalance, traderEquity.RemainingTraderBalance);
        }

        /// <summary>
        /// Function to test buy equity when trader is buying equity first time equity
        /// </summary>
        [Fact]
        public void BuyEquity_BuyingEquityThatDonotExist_Success()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 1, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);
            int equityId = 2;
            int qty = 100;
            double expectedTraderBalance = _traderFund.RemainingBalance - (qty * _equity2.Price);

            // Act
            // As per arrange the trader has never bought this equity
            var traderEquity = traderFundService.BuyEquity(equityId, qty);

            // Assert
            Assert.NotNull(traderEquity);
            Assert.Equal(100, traderEquity.Quantity);
            Assert.Equal(_equity2.Price * qty, traderEquity.TotalCost);
            Assert.Equal(expectedTraderBalance, traderEquity.RemainingTraderBalance);
        }

        #endregion

        #region Test Methods for SellEquity

        /// <summary>
        /// Function to test sell equity at non trading hours
        /// </summary>
        [Fact]
        public void SellEquity_SellingWithInsufficientBalance_RaiseException()
        {
            // Arrange
            _traderFundRepositoryMoq.Setup(repo => repo.GetById(_traderFund.Id)).Returns(new TraderFund { Id = 1, RemainingBalance = 5 });
            _equityRepositoryMoq.Setup(repo => repo.GetById(_equity1.Id)).Returns(new Equity { Id = 1, EquityName = "Testing", Price = 5 });
            _traderEquityRepositoryMoq.Setup(repo => repo.Get(_traderFund.Id, _equity1.Id)).Returns(new TraderEquity { Id = 1, TraderId = 1, EquityId = 1, Quantity = 5 });
            DateTimeHelper.Set(new DateTime(2021, 12, 14, 12, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.SellEquity(1, 1));
            Assert.Equal("Insufficient Fund value", ex.Message);
        }

        /// <summary>
        /// Function to test sell equity at non trading hours
        /// </summary>
        [Fact]
        public void SellEquity_SellingAtNonTradingHour_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 11, 15, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.SellEquity(1, 120));
            Assert.Equal("Time is not eligible for buying equity", ex.Message);
        }

        /// <summary>
        /// Function to test sell equity that were never bought
        /// </summary>
        [Fact]
        public void SellEquity_SellingEquityThatDontExist_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 13, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.SellEquity(2, 120));
            Assert.Equal("Trader don't hold this equity", ex.Message);
        }

        /// <summary>
        /// Function to test sell equity whose 0 qty are held by trader
        /// </summary>
        [Fact]
        public void SellEquity_SellingEquityWithZeroQuantity_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 13, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.SellEquity(3, 120));
            Assert.Equal("Trader don't hold this equity", ex.Message);
        }

        /// <summary>
        /// Function to test sell equity whose with unsufficient qty are held by trader
        /// </summary>
        [Fact]
        public void SellEquity_SellingEquityWithUnSufficientQuantity_RaiseException()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 13, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);

            // Act and Assert
            Exception ex = Assert.Throws<Exception>(() => traderFundService.SellEquity(1, 120));
            Assert.Equal("Insufficient Equity Quantity", ex.Message);
        }

        /// <summary>
        /// Functionto test Selling equity when it is success and brokerage is 20
        /// </summary>
        [Fact]
        public void SellEquity_SellingEquityWithBrokerage20_Success()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 13, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);
            int equityId = 1;
            int qty = 50;
            double brokerage = 20;
            double expectedTraderBalance = 50000 + (100 * 50) - brokerage;

            // Act
            // As per arrange the trader has 100 quantity of the equity with id 1
            var traderEquity = traderFundService.SellEquity(equityId, qty);


            // Assert
            Assert.NotNull(traderEquity);
            Assert.Equal(50, traderEquity.Quantity);
            Assert.Equal(brokerage, traderEquity.TotalBrokerage);
            Assert.Equal(expectedTraderBalance, traderEquity.RemainingTraderBalance);
        }

        /// <summary>
        /// Functionto test Selling equity when it is success and brokerage is greater than 20
        /// </summary>
        [Fact]
        public void SellEquity_SellingEquityWithBrokerageGreaterThan20_Success()
        {
            // Arrange
            DateTimeHelper.Set(new DateTime(2021, 12, 13, 10, 13, 15));
            var traderFundService = new TraderEquityService(_traderFundRepositoryMoq.Object, _equityRepositoryMoq.Object, _traderEquityRepositoryMoq.Object);
            int equityId = 4;
            int qty = 50;
            double brokerage = 37.5;
            double expectedTraderBalance = 50000 + (1500 * 50) - brokerage;

            // Act
            // As per arrange the trader has 100 quantity of the equity with id 4
            var traderEquity = traderFundService.SellEquity(equityId, qty);


            // Assert
            Assert.NotNull(traderEquity);
            Assert.Equal(9950, traderEquity.Quantity);
            Assert.Equal(brokerage, traderEquity.TotalBrokerage);
            Assert.Equal(expectedTraderBalance, traderEquity.RemainingTraderBalance);
        }

        #endregion
    }
}
