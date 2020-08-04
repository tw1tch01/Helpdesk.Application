using System;
using Helpdesk.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Helpdesk.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {
        public Guid UserGuid { get; set; }
    }
}