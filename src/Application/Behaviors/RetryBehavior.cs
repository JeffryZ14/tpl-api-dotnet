using MediatR;
using Polly;
using Polly.Retry;

namespace Application.Behaviors
{
    public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly AsyncRetryPolicy _retryPolicy;

        public RetryBehavior()
        {
            _retryPolicy = Policy
                .Handle<Exception>(ex => IsTransient(ex))
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return await _retryPolicy.ExecuteAsync(_ => next(), cancellationToken);
        }

        private bool IsTransient(Exception ex)
        {
            return true;
        }
    }
}
