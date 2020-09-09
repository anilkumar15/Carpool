using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace Carpool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RidesController : ControllerBase
    {
        private static readonly string[] rides = new[]
        {
            "Bellevue-Seattle", "Bellevue-Redmond", "Seattle-Redmond"
        };

        private readonly ILogger<RidesController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "Rides.Read" };

        private readonly GraphServiceClient _graphServiceClient;

        public RidesController(ILogger<RidesController> logger)
        {
             _logger = logger;
       }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return rides;
        }
    }
}
