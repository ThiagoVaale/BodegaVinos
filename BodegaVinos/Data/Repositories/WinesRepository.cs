using BodegaVinos.Entities;

namespace BodegaVinos.Data.Repositories
{
    public class WinesRepository
    {
        public List<WineEntity> Wines { get; set; } = new List<WineEntity>()
        {
            new WineEntity()
            {
                Id = 1, 
                Name = "Luigi Bosca Malbec", 
                Variety = "Malbec", 
                Year = 2021, 
                Region = "Mendoza", 
                Stock = 25
            }, 
            new WineEntity()
            {
                Id = 2,
                Name = "Catena Zapata Cabernet Sauvignon",
                Variety = "Cabernet Sauvignon",
                Year = 2000,
                Region = "Mendoza",
                Stock = 5
            }, 
            new WineEntity()
            {
                Id = 3,
                Name = "El Enemigo Bonarda",
                Variety = "Bonarda",
                Year = 2010,
                Region = "San Juan",
                Stock = 0
            }
        };
    }
}
