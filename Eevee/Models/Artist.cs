using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class Artist
    {
        private string _word_vector;

        public int ArtistID { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Description { get; set; }

        public int Listens { get; set; } = 0;

        public float Rating { get; set; } = 1.0f;

        public string WordVec { get; set; }

    }
}
