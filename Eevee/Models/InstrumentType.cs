using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class InstrumentType
    {
        public int InstrumentTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
    }
}
