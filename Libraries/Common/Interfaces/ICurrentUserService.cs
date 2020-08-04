using System;

namespace Helpdesk.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserGuid { get; }
    }
}