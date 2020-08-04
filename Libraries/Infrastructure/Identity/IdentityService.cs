using System;
using System.Threading.Tasks;
using Helpdesk.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IUser> GetUserAsync(string username)
        {
            return await _userManager.Users.FirstAsync(u => u.UserName == username);
        }

        public async Task<IUser> GetUserAsync(Guid userGuid)
        {
            return await _userManager.Users.FirstAsync(u => u.UserGuid == userGuid);
        }
    }
}