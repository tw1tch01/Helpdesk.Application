using System;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Application.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehaviour(
            ILogger<TRequest> logger,
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userGuid = _currentUserService.UserGuid;
            var user = await _identityService.GetUserAsync(userGuid);
            _logger.LogInformation("Request: {Name} {@userGuid} {@username} {@Request}", requestName, userGuid, user?.UserName, request);
        }
    }
}