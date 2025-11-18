using Application.Common;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ErrorOr<Guid >>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public CreateProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<ErrorOr<Guid >> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Money, _currentUser.UserId);
            await _unitOfWork.Products.AddAsync(product, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return product.Id.Value;
        }
    }
}
