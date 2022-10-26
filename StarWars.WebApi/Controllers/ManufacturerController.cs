using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StarWars.WebApi.Controllers
{
    [ApiController]
    [Route("api/manufacturer")]
    [EnableCors("MyPolicy")]
    [Authorize]
    public class ManufacturerController : ControllerBase
    {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IStarWarsService _StarWarsService;

        public ManufacturerController(ILogger<ManufacturerController> logger, IStarWarsService StarWarsService)
        {
            _logger = logger;
            _StarWarsService = StarWarsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _StarWarsService.GetManufacturer();
            
            return Ok(model);
        }
    }
}