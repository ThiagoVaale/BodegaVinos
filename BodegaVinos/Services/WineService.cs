using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Repositories;
using BodegaVinos.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BodegaVinos.Services
{
    public class WineService
    {
        private readonly WinesRepository _winesRepository;

        public WineService(WinesRepository winesRepository)
        {
            _winesRepository = winesRepository;
        }

        public List<WineEntity> GetAllWines()
        {
            return _winesRepository.Wines;
        }

        public List<WineEntity> GetAvailabilityWines()
        {
            {
               return _winesRepository.Wines.Where(aw => aw.Stock > 0).ToList();
            };
        }
        public List<WineEntity> RegisterNewWine(RegisterNewWineDto RegisterWineDto)
        {
            WineEntity wineRegister = new WineEntity()
            {
                Name = RegisterWineDto.Name,
                Variety = RegisterWineDto.Variety,
                Year = RegisterWineDto.Year,
                Region = RegisterWineDto.Region,
                Stock = RegisterWineDto.Stock
            };
            _winesRepository.Wines.Add(wineRegister);
            return _winesRepository.Wines;
        }
    }
}
