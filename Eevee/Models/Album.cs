using System.ComponentModel.DataAnnotations;


namespace Eevee.Models
{
    public class Album
    {
        public int AlbumID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Artist Artist { get;  set; }
    }
}
