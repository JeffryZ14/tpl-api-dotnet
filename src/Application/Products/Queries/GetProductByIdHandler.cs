
using Application.Products.Dtos;
using Application.Products.Queries;
using AutoMapper;
using Domain.Abstractions;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var idVo = ProductId.From(request.Id);
            var product = await _unitOfWork.Products.GetByIdAsync(idVo, ct);

            if (product is null)
                return Errors.Product.NotFound(request.Id);

            var dto = _mapper.Map<ProductDto>(product);
            return dto;
        }
    }
}
