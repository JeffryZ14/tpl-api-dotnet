using API.Models.Products;
using Application.Products.Commands;
using Application.Products.Queries;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly ISender _mediator;
        public ProductsController(ISender mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest dto)
        {
            var nameVo = new ProductName(dto.Name);
            var priceVo = new Money(dto.Price, dto.Currency);

            var cmd = new CreateProductCommand(nameVo, priceVo);
            var result = await _mediator.Send(cmd);

           return result.Match(
            res => Ok(res),
            errors => Problem(errors)
        );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
            res => Ok(res),
            errors => Problem(errors));
        }

        [HttpPut("{id:guid}/price")]
        public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdateProductPriceRequest dto)
        {
            var priceVo = new Money(dto.Price, dto.Currency);
            var cmd = new UpdateProductPriceCommand(id, priceVo);
            var result = await _mediator.Send(cmd);

            return result.Match(
            res => Ok(res),
            errors => Problem(errors));
        }

        [HttpPost("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var cmd = new DeactivateProductCommand(id);
            var result = await _mediator.Send(cmd);

            return result.Match(
            res => Ok(res),
            errors => Problem(errors));
        }
    }
}
