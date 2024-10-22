using System.ComponentModel.DataAnnotations;

namespace BodegaVinos.Common.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
