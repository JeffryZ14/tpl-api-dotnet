using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class AuditLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<AuditLoggingBehavior<TRequest, TResponse>> _logger;

        public AuditLoggingBehavior(ILogger<AuditLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var startTime = DateTime.UtcNow;

            _logger.LogInformation("[Audit] Started handling {RequestName} at {StartTime}", requestName, startTime);

            var response = await next();

            var endTime = DateTime.UtcNow;
            _logger.LogInformation(
                "[Audit] Finished handling {RequestName} at {EndTime}. Duration: {Duration}ms",
                requestName,
                endTime,
                (endTime - startTime).TotalMilliseconds);

            return response;
        }
    }
}
