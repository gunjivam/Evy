using Eevee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee.Models
{
    public class History
    {
        public int HistoryID {get; set;}

        [Required]
        public User User { get; set; }

        [Required]
        public Song Song { get; set; }

        [Required]
        public float Progress { get; set; }
    }
}
