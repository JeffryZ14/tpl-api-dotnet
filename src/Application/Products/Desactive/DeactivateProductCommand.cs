using ErrorOr;
using MediatR;

namespace Application.Products.Commands
{
    public record DeactivateProductCommand(Guid Id) : IRequest<ErrorOr<bool>>;
}
