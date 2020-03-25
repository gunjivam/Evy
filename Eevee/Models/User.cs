using System.ComponentModel.DataAnnotations;

namespace Eevee.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
    }
}
