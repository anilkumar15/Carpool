using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Carpool.Models;

namespace Carpool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RidersController : ControllerBase
    {
        private readonly ILogger<RidersController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "Riders.Read" };

        private readonly GraphServiceClient _graphServiceClient;

        public RidersController(ILogger<RidersController> logger,
                                         GraphServiceClient graphServiceClient)
        {
             _logger = logger;
            _graphServiceClient = graphServiceClient;
       }

        [HttpGet]
        public async Task<IEnumerable<Stakeholder>> Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var users = await _graphServiceClient.Users.Request().GetAsync();

            return users.Select(u => new Stakeholder { DisplayName = u.DisplayName, PhoneNumber = u.MobilePhone });
        }
    }
}
