using MagicVanilla_CouponAPI.Models;
namespace MagicVanilla_CouponAPI.Data
{
    public static class CouponStore
    {
        public static List<Coupon> couponList = new List<Coupon>
        {
            new Coupon
            {
                Id = 1,
                Name = "100OFF",
                PercentOff = 10,
                IsActive = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            },
            new Coupon
            {
                Id = 2,
                Name = "50OFF",
                PercentOff = 5,
                IsActive = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            },
            new Coupon
            {
                Id = 3,
                Name = "150OFF",
                PercentOff = 15,
                IsActive = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            }
        };
    }
}
