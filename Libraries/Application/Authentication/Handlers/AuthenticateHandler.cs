using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Authentication.Pings;
using Helpdesk.Common.Interfaces;
using MediatR;

namespace Helpdesk.Application.Authentication.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticatePing>
    {
        public AuthenticateHandler(IIdentityService identityService)
        {

        }

        public Task<Unit> Handle(AuthenticatePing request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
