using FluentValidation;
using MagicVanilla_CouponAPI.Models.DTO;
using System.Reflection;

namespace MagicVanilla_CouponAPI.Models.Validation
{
    public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.PercentOff).InclusiveBetween(0, 100);
            //RuleFor(c => c.IsActive).NotNull();
        }
    }
}
