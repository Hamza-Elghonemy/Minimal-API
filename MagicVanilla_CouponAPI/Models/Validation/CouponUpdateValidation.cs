using FluentValidation;
using MagicVanilla_CouponAPI.Models.DTO;

namespace MagicVanilla_CouponAPI.Models.Validation
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
    {
        public CouponUpdateValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required").GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.PercentOff).NotEmpty().WithMessage("PercentOff is required");
            //RuleFor(c => c.IsActive).NotEmpty().WithMessage("IsActive is required");
            //RuleFor(c => c.LastUpdated).NotEmpty().WithMessage("LastUpdated is required");
        }
    }
    
}
