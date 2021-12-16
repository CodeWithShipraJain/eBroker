using eBroker.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eBroker.Controllers
{
    /// <summary>
    /// Trader Equity Controller
    /// </summary>
    [ApiController]
    public class TraderEquityController
    {
        #region Properties and Contructor

        /// <summary>
        /// Trader Equity Service
        /// </summary>
        private ITraderEquityService _tradeEquityService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeEquityService">Trade Equity Service</param>
        public TraderEquityController(ITraderEquityService tradeEquityService)
        {
            _tradeEquityService = tradeEquityService;
        }

        #endregion

        /// <summary>
        /// End point to add the given quantity of equity against trader account
        /// </summary>
        /// <param name="equityId">Equity Id to be bought</param>
        /// <param name="quantity">Quantity to be bought</param>
        /// <returns>TraderEquityBuyDto</returns>
        [Route("/api/trader/equity/{equityId}/buy/{quantity}")]
        [HttpPost]
        public IActionResult Buy(int equityId, int quantity)
        {
            try
            {
                return new OkObjectResult(_tradeEquityService.BuyEquity(equityId, quantity));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// End point to sell the given quantity of equity against trader account
        /// </summary>
        /// <param name="equityId">Equity Id to be sold</param>
        /// <param name="quantity">Quantity to be sold</param>
        /// <returns>TraderEquitySellDto</returns>
        [Route("/api/trader/equity/{equityId}/sell/{quantity}")]
        [HttpPost]
        public IActionResult Sell(int equityId, int quantity)
        {
            try
            {
                return new OkObjectResult(_tradeEquityService.SellEquity(equityId, quantity));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
