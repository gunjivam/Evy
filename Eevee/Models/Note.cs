using Eevee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee.Models
{
    public class Note
    {
        public int NoteID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public InstrumentType InstrumentType { get; set; }

        [Required]
        public Frequency Frequency { get; set; } 
    }
}
