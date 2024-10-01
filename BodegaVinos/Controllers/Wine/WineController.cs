using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Repositories;
using BodegaVinos.Entities;
using BodegaVinos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BodegaVinos.Controllers.Wine
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private readonly WineService _wineService;

        public WineController(WineService wineService)
        {
            _wineService = wineService;
        }

        [HttpGet]
        public IActionResult GetAllWines()
        {
            return  Ok(_wineService.GetAllWines());
        }

            [HttpGet("/Vinos disponibles")]
            public IActionResult GetWineAvailability()
            {
                return Ok(_wineService.GetAvailabilityWines());
            }

        [HttpPost]
        public IActionResult RegisterNewWine([FromBody] RegisterNewWineDto registerNewWineDto)
        {
            if (registerNewWineDto.Name == null)
            {
                return BadRequest("El nombre del vino es OBLIGATORIO");
            }
            else if (registerNewWineDto.Year < 1990 || registerNewWineDto.Year > 2024)
            {
                return BadRequest("El año del vino tiene que estar entre 1990 y 2024");
            }
            else
            {
                return Ok(_wineService.RegisterNewWine(registerNewWineDto));
            }
        }
    }   
}
