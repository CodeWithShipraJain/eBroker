using eBroker.Controllers;
using eBroker.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace eBroker.Test
{
    /// <summary>
    /// Fund Controller Tests.
    /// </summary>
    public class FundControllerTests
    {
        #region Properties and Contructor

        /// <summary>
        /// Trader Fund Moq Repository
        /// </summary>
        Mock<ITradeFundService> traderFundServiceMoq;

        /// <summary>
        /// Contructor
        /// </summary>
        public FundControllerTests()
        {
            traderFundServiceMoq = new Mock<ITradeFundService>();
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Function to test the controller when exception is raised
        /// </summary>
        [Fact]
        public void Add_AddFundsRaiseException_HandleException()
        {
            // Arrange
            traderFundServiceMoq.Setup(repo => repo.AddFunds(It.IsAny<double>())).Throws(new Exception("Some Issue Occured"));
            var controller = new FundController(traderFundServiceMoq.Object);

            // Act
            var result = controller.Add(50000);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Some Issue Occured", ((BadRequestObjectResult)result).Value);
        }



        /// <summary>
        /// Function to test the controller when request is processed
        /// </summary>
        [Fact]
        public void Add_AddFunds_HandleResult()
        {
            // Arrange
            traderFundServiceMoq.Setup(repo => repo.AddFunds(It.IsAny<double>())).Returns(new Service.Dto.TraderFundDto { Id = 1, RemainingBalance= 50000 });
            var controller = new FundController(traderFundServiceMoq.Object);

            // Act
            var result = controller.Add(50000);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.IsType<Service.Dto.TraderFundDto>(((OkObjectResult)result).Value);
        }

        #endregion
    }
}
