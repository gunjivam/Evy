using Eevee.Models;
using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class SongFrequencyAssignment
    {
        public int SongFrequencyAssignmentID { get; set; }

        [Required]
        public Song Song { get; set; }

        [Required]
        public Frequency Frequency { get; set; }
    }
}
