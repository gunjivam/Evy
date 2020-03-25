using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee.Models
{
    public class InstrumentManufacturer
    {
        public int InstrumentManufacturerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
    }
}
