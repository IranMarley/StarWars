using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StarWars.WebApi.Controllers
{
    [ApiController]
    [Route("api/starship")]
    [EnableCors("MyPolicy")]
    [Authorize]
    public class StarshipController : ControllerBase
    {
        private readonly ILogger<StarshipController> _logger;
        private readonly IStarWarsService _StarWarsService;

        public StarshipController(ILogger<StarshipController> logger, IStarWarsService StarWarsService)
        {
            _logger = logger;
            _StarWarsService = StarWarsService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] StarWarsFilterModel filter)
        {
            var model = _StarWarsService.GetStarShips(filter);
            
            return Ok(model);
        }
    }
}