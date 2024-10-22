using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Context;
using BodegaVinos.Entities;

namespace BodegaVinos.Data.Repositories
{
    public class WinesRepository
    {
        private readonly WineDbContext _wineDbContext;

        public WinesRepository(WineDbContext wineDbContext)
        {
            _wineDbContext = wineDbContext;
        }
        public List<WineEntity> GetAllWines()
        {
            return _wineDbContext.Wines.ToList();
        }

        public List<WineEntity> GetAvailabilityWines()
        {
            return _wineDbContext.Wines.Where(aw => aw.Stock > 0).ToList();
        }

        public List<WineEntity> VarietyWines(string variety)
        {
            return _wineDbContext.Wines.Where(v => v.Variety.Contains(variety)).ToList();
        }

        public List<WineEntity> RegisterNewWine(RegisterNewWineDto registerWineDto)
        {
            WineEntity wineRegister = new WineEntity()
            {
                Name = registerWineDto.Name,
                Variety = registerWineDto.Variety,
                Year = registerWineDto.Year,
                Region = registerWineDto.Region,
                Stock = registerWineDto.Stock
            };
            _wineDbContext.Wines.Add(wineRegister);
            _wineDbContext.SaveChanges(); //SaveChanges() nos guarda los datos del nuevo vino para luego retornar a esa lista
            return _wineDbContext.Wines.ToList();
        }

        public WineEntity? UpdateWineStock(int idWineForUpdate, int newStock)
        {
            WineEntity? idWineUpdate = _wineDbContext.Wines.FirstOrDefault(i => i.Id == idWineForUpdate);

            if (idWineUpdate == null)
            {
                return null;
            }

            idWineUpdate.Stock = newStock;

            _wineDbContext.Wines.Update(idWineUpdate);
            _wineDbContext.SaveChanges();
            return idWineUpdate;
        }
    }
}
