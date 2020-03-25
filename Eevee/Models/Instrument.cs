using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class Instrument
    {
        public int InstrumentID { get; set; }

        [Required]
        public string Name { get; set; }

        public int Count { get; set; } = 0;

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }


        [Required]
        public Genre Genre { get; set; }

        [Required]
        public InstrumentManufacturer InstrumentManufacturer { get; set; }

        [Required]
        public InstrumentType InstrumentType { get; set; }
    

    }
}
