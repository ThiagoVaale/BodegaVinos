using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Context;
using BodegaVinos.Data.Repositories;
using BodegaVinos.Entities;
using BodegaVinos.Exeptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BodegaVinos.Services
{
    public class WineService
    {
        private readonly WinesRepository _wineRepository;
        public WineService(WinesRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        public List<WineEntity> GetAllWines()
        {
            return _wineRepository.GetAllWines();
        }

        public List<WineEntity> GetAvailabilityWines()
        {
            return _wineRepository.GetAvailabilityWines();
        }

        public List<WineEntity> VarietyWines(string variety)
        {
            return _wineRepository.VarietyWines(variety);
        }
        public List<WineEntity> RegisterNewWine(RegisterNewWineDto registerNewWineDto)
        {
            if (registerNewWineDto.Name == null)
            {
                throw new WineException("El nombre del vino es OBLIGATORIO");
            }
            else
            {
                return _wineRepository.RegisterNewWine(registerNewWineDto);
            }
        }

        public WineEntity? UpdateWineStock(int idWineForUpdate, int newStock)
        {
            WineEntity? updateWineId = _wineRepository.UpdateWineStock(idWineForUpdate, newStock);

            if (updateWineId == null)
            {
                throw new WineException($"El id {idWineForUpdate} no EXISTE");
            }
            else
            {
                return _wineRepository.UpdateWineStock(idWineForUpdate, newStock);
            }
           
        }
    }
}
