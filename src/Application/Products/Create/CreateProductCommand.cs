using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Commands
{
    public record CreateProductCommand(ProductName Name, Money Money) : IRequest<ErrorOr<Guid>>;
}
