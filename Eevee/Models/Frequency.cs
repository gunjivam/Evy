using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class Frequency
    {
        public int FrequencyID { get; set; }

        [Required]
        public int Hz { get; set; }
    }
}
