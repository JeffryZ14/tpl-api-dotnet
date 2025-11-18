
using Application.Common;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Handlers
{
    public class DeactivateProductHandler : IRequestHandler<DeactivateProductCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeactivateProductHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }
        public async Task<ErrorOr<bool>> Handle(DeactivateProductCommand request, CancellationToken ct)
        {
            var idVo = ProductId.From(request.Id);
            var product = await _unitOfWork.Products.GetByIdAsync(idVo, ct);

            if (product is null)
                return Errors.Product.NotFound(request.Id);

            if (!product.IsActive)
                return Errors.Product.AlreadyDeactivated(request.Id);

            product.Deactivate();
            //product.SetModified(_currentUser.UserId);

            return true;
        }
    }
}
