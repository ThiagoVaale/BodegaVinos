using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BodegaVinos.Entities
{
    public class CataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTesting { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public List<WineEntity> Wines { get; set; } = new List<WineEntity>(); //Voy a configurar una relacion de 1 : N donde el 1 es la cata y el N los vinos.
        public List<string> InvitedPeople { get; set; } = new List<string>();
    }
}
