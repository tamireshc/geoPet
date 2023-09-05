using geoPet.Entities;
using geoPet.Services;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private PositionService _positionService;
        public PositionController(PositionService service) { 
            this._positionService = service;
        }
        [HttpPost]
        public IActionResult PostPosition(PositionRequest positionRequest) 
        {
            _positionService.PostPosition(positionRequest);
            return Ok();
        }
    }
}

