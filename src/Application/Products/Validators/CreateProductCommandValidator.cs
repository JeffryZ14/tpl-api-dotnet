using Application.Products.Commands;
using FluentValidation;

namespace Application.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name.Value).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Money.Amount).GreaterThan(0);
        }
    }

    public class UpdateProductPriceCommandValidator : AbstractValidator<UpdateProductPriceCommand>
    {
        public UpdateProductPriceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Money.Amount).GreaterThan(0);
            RuleFor(x => x.Money.Currency).NotEmpty().Length(3);
        }
    }

    public class DeactivateProductCommandValidator : AbstractValidator<DeactivateProductCommand>
    {
        public DeactivateProductCommandValidator() => RuleFor(x => x.Id).NotEmpty();
    }

}
