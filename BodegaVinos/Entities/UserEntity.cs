using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BodegaVinos.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre de usuario, requerido y único
        public string Username { get; set; } = string.Empty;

        // Contraseña, al menos 8 caracteres
        public string Password { get; set; }
    }
}
