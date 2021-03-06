﻿using System.Linq;
using Helpdesk.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Identity.Controllers
{
    [ApiVersion(ApiConfig.CurrentVersion)]
    [Authorize]
    [Route("identity")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IdentityController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}