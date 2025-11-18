using Application.Products.Dtos;
using ErrorOr;
using MediatR;

namespace Application.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ErrorOr<ProductDto>>;
}
