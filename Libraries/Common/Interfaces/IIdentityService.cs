using System;
using System.Threading.Tasks;

namespace Helpdesk.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<IUser> GetUserAsync(string username);

        Task<IUser> GetUserAsync(Guid userGuid);
    }
}