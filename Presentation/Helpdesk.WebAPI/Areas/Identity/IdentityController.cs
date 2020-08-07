using System.Linq;
using Helpdesk.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Identity
{
    [ApiVersion(ApiConfig.CurrentVersion)]
    [Authorize]
    [Route("identity")]
    public class IdentityController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}