using eBroker.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eBroker.Controllers
{
    /// <summary>
    /// Fund Controller
    /// </summary>
    [ApiController]
    public class FundController
    {
        #region Properties and Contructor

        /// <summary>
        /// Trade Fund Service
        /// </summary>
        private ITradeFundService _tradeFundService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tradeFundService">Trade Fund Service</param>
        public FundController(ITradeFundService tradeFundService)
        {
            _tradeFundService = tradeFundService;
        }

        #endregion

        /// <summary>
        /// End point to add the amount against the trader account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [Route("/api/fund/add/{amount}")]
        [HttpPost]
        public IActionResult Add(double amount)
        {
            try
            {
                return new OkObjectResult(_tradeFundService.AddFunds(amount));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
