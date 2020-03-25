using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class Advertiser
    {
        public int AdvertiserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public long CardNumber { get; set; }

        [Required]
        public int MonthsRemaining { get; set; }

        [Required]
        public bool AutoPay { get; set; } = false;

        [Required]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
      
        public int Clicks { get; set; } = 0;
        
        public int Views { get; set; } = 0;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public AdvertiserType AdvertiserType { get; set; }
    }
}
