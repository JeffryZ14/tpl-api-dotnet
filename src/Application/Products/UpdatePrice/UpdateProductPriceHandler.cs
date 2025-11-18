using Application.Common;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.Common;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Handlers
{
    public class UpdateProductPriceHandler : IRequestHandler<UpdateProductPriceCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public UpdateProductPriceHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }


        public async Task<ErrorOr<bool>> Handle(UpdateProductPriceCommand request, CancellationToken ct)
        {
            var idVo = ProductId.From(request.Id);
            var product = await _unitOfWork.Products.GetByIdAsync(idVo, ct);

            if (product is null)
                return Errors.Product.NotFound(request.Id);

            try
            {
                product.UpdatePrice(new Money(request.Money.Amount, request.Money.Currency), _currentUser.UserId);
            }
            catch (BussinessException ex)
            {
                return Error.Validation(code: "Product.InvalidPrice", description: ex.Message);
            }

            return true;
        }
    }
}
