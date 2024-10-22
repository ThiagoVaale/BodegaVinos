using System.ComponentModel.DataAnnotations;

namespace BodegaVinos.Common.Dtos
{
    public class UpdateWineDTO
    {
        [Required]
        public string Name { get; set; }
        public string Variety { get; set; }
        [Required]
        [Range(1990, 2024)]
        public int Year { get; set; }
        public string Region { get; set; }
        public int Stock { get; set; }
    }
}
