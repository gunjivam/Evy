using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }

        [Required]
        public string Name { get; set; } = "Playlist";

        [Required]
        public User User { get; set; }
    }
}
