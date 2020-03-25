using Eevee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee.Models
{
    public class SongInstrumentAssignment
    {
        public int SongInstrumentAssignmentID { get; set; }

        [Required]
        public int SongID { get; set; }

        [Required]
        public Instrument Instrument { get; set; }
    }
}
