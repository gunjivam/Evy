using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class AdvertiserType
    {
        public int AdvertiserTypeID { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
