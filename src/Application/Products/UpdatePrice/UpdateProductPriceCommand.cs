using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Commands
{

    public record UpdateProductPriceCommand(Guid Id, Money Money) : IRequest<ErrorOr<bool>>;
}
