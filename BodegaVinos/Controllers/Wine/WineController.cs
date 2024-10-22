using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Repositories;
using BodegaVinos.Entities;
using BodegaVinos.Exeptions;
using BodegaVinos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return Ok(_wineService.GetAllWines());
        }

        [HttpGet("/vinos-disponibles")]
        public IActionResult GetWineAvailability()
        {
            return Ok(_wineService.GetAvailabilityWines());
        }

        [HttpGet("{variety}")]
        public IActionResult VarietyWines([FromRoute] string variety)
        {
            return Ok(_wineService.VarietyWines(variety));
        }

        [HttpPost]
        public IActionResult RegisterNewWine([FromBody] RegisterNewWineDto registerNewWineDto)
        {
            try
            {
                return Ok(_wineService.RegisterNewWine(registerNewWineDto));
            } 
            catch (WineException error)
            {
                return BadRequest(error.Message);
            }
        }
         
        [HttpPut("{idWineForUpdate}/stock")]
        public IActionResult UpdateWineStock([FromRoute]int idWineForUpdate, [FromBody] int newStock)
        {
            try
            {
                return Ok(_wineService.UpdateWineStock(idWineForUpdate, newStock));
            }
            catch (WineException error)
            {
                return BadRequest(error.Message);
            }
            
        }
    }   
}
