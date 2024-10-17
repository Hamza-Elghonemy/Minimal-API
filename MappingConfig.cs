using AutoMapper;
using MagicVanilla_CouponAPI.Models;
using MagicVanilla_CouponAPI.Models.DTO;

namespace MagicVanilla_CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();
        }
    }
}
