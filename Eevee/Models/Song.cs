using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NaturalLanguage;

namespace Eevee.Models
{
    public class Song
    {
        public int SongID { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Duration { get; set; }

        public int Listens { get; set; } = 0;

        public int Rating { get; set; } = 1;

        public string Lyrics { get; set; }

        public string WordVec { get; set; }

        //[Required]
        public Genre Genre { get; set; }

        //[Required]
        public Album Album { get; set; }

        //[Required]
        public string Fp { get; set; }

    }
}
