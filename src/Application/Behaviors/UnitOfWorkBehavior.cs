
using Domain.Abstractions;
using MediatR;

namespace Application.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var response = await next();

            await _unitOfWork.DispatchDomainEventsAsync(cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}