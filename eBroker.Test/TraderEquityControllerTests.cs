using eBroker.Controllers;
using eBroker.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Test
{
    /// <summary>
    /// Trader Equity Controller Tests.
    /// </summary>
    public class TraderEquityControllerTests
    {
        #region Properties and Contructor

        /// <summary>
        /// Trader Equity Moq Repository
        /// </summary>
        Mock<ITraderEquityService> traderEquityServiceMoq;

        /// <summary>
        /// Contructor
        /// </summary>
        public TraderEquityControllerTests()
        {
            traderEquityServiceMoq = new Mock<ITraderEquityService>();
        }

        #endregion

        #region Buy Methods Test

        /// <summary>
        /// Function to test the controller when trader buy equity end point throws exception
        /// </summary>
        [Fact]
        public void Buy_BuyEquityRaiseException_HandleException()
        {
            // Arrange
            traderEquityServiceMoq.Setup(repo => repo.BuyEquity(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Some Issue Occured"));
            var controller = new TraderEquityController(traderEquityServiceMoq.Object);

            // Act
            var result = controller.Buy(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Some Issue Occured", ((BadRequestObjectResult)result).Value);
        }



        /// <summary>
        /// Function to test the controller when trader buy equity request is processed successfully
        /// </summary>
        [Fact]
        public void Buy_BuyEquity_HandleResult()
        {
            // Arrange
            traderEquityServiceMoq.Setup(repo => repo.BuyEquity(It.IsAny<int>(), It.IsAny<int>())).Returns(new Service.Dto.TradeEquityBuyDto());
            var controller = new TraderEquityController(traderEquityServiceMoq.Object);

            // Act
            var result = controller.Buy(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.IsType<Service.Dto.TradeEquityBuyDto>(((OkObjectResult)result).Value);
        }

        #endregion

        #region Sell Methods Test

        /// <summary>
        /// Function to test the controller when trader sell equity end point throws exception
        /// </summary>
        [Fact]
        public void Sell_SellEquityRaiseException_HandleException()
        {
            // Arrange
            traderEquityServiceMoq.Setup(repo => repo.SellEquity(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Some Issue Occured"));
            var controller = new TraderEquityController(traderEquityServiceMoq.Object);

            // Act
            var result = controller.Sell(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Some Issue Occured", ((BadRequestObjectResult)result).Value);
        }



        /// <summary>
        /// Function to test the controller when trader sell equity request is processed successfully
        /// </summary>
        [Fact]
        public void Sell_SellEquity_HandleResult()
        {
            // Arrange
            traderEquityServiceMoq.Setup(repo => repo.SellEquity(It.IsAny<int>(), It.IsAny<int>())).Returns(new Service.Dto.TraderEquitySellDto());
            var controller = new TraderEquityController(traderEquityServiceMoq.Object);

            // Act
            var result = controller.Sell(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.IsType<Service.Dto.TraderEquitySellDto>(((OkObjectResult)result).Value);
        }

        #endregion
    }
}
