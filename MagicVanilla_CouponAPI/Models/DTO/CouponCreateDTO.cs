using System.ComponentModel.DataAnnotations;

namespace MagicVanilla_CouponAPI.Models.DTO
{
    public class CouponCreateDTO
    {
        public string Name { get; set; }
        public int PercentOff { get; set; }
        public bool IsActive { get; set; }
    }
}
