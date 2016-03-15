﻿using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;
using System;
using Coupa.PunchoutModule.Web.Services;

namespace Coupa.PunchoutModule.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/punchout/coupa")]
    public class CoupaPunchoutController : ApiController
    {
        private readonly IPunchoutService _punchoutService;

        public CoupaPunchoutController(IPunchoutService punchoutService)
        {
            _punchoutService = punchoutService;
        }

        [HttpPost]
        [Route("setup")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PunchoutSetup()
        {
            var setupRequest = await Request.Content.ReadAsStringAsync();
            var punchoutGateways = _punchoutService.GetAllPunchoutGateways();
            if (punchoutGateways != null && punchoutGateways.Any(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase)))
            {
                var coupaGateway = punchoutGateways.First(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase));
                var retVal = coupaGateway.PunchoutSetup(setupRequest);
                
                return Ok(retVal);
            }
            return NotFound();
        }

        /// <summary>
        /// Send shopping cart to coupa system
        /// </summary>
        /// <param name="cartId">Shopping cart id</param>
        [HttpGet]
        [Route("ordermessage")]
        [AllowAnonymous]
        public IHttpActionResult SendOrderMessage(string cartId)
        {
            var punchoutGateways = _punchoutService.GetAllPunchoutGateways();
            if (punchoutGateways != null && punchoutGateways.Any(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase)))
            {
                var coupaGateway = punchoutGateways.First(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase));
                var retVal = coupaGateway.PunchoutOrderMessage(cartId);
                if (!string.IsNullOrEmpty(retVal))
                    return Ok(retVal);
            }

            return NotFound();
        }

        /// <summary>
        /// Create order by received order data
        /// </summary>
        [HttpPost]
        [Route("createorder")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CreateOrder()
        {
            var createOrderRequest = await Request.Content.ReadAsStringAsync();
            var punchoutGateways = _punchoutService.GetAllPunchoutGateways();
            if (punchoutGateways != null && punchoutGateways.Any(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase)))
            {
                var coupaGateway = punchoutGateways.First(x => x.Name.Equals("Coupa", StringComparison.InvariantCultureIgnoreCase));
                var response = coupaGateway.CreateOrder(createOrderRequest);

                return Ok(response);
            }

            return NotFound();
        }
    }
}