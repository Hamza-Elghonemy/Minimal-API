namespace MagicVanilla_CouponAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PercentOff { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
}
 