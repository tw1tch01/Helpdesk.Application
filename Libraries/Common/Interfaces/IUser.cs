using System;

namespace Helpdesk.Common.Interfaces
{
    public interface IUser
    {
        Guid UserGuid { get; set; }
        string UserName { get; set; }
    }
}