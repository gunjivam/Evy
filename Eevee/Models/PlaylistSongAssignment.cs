using Eevee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee.Models
{
    public class PlaylistSongAssignment
    {
        public int PlaylistSongAssignmentID { get; set; }

        [Required]
        public Playlist Playlist { get; set; }

        [Required]
         public Song Song { get; set; }
    }
}
