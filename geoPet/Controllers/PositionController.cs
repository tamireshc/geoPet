using geoPet.Entities;
using geoPet.Models;
using geoPet.Services;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private PositionService _positionService;
        public PositionController(PositionService service)
        {
            this._positionService = service;
        }

        [HttpPost]
        public IActionResult post(PositionRequest positionRequest)
        {
            _positionService.post(positionRequest);
            return Ok();
        }

        [HttpGet]
        public IActionResult findAll()
        {
            List<Position> positions = _positionService.findAll();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public IActionResult findById(int id)
        {
            Position position = _positionService.findById(id);
            return Ok(position);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            _positionService.delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult update(int id, PositionRequest request)
        {
            _positionService.update(id, request);
            return Ok();
        }

        [HttpGet("Pet/{id}")]
        public IActionResult lastPositionOfPet(int id)
        {
            Position position = _positionService.lastPositionOfPet(id);
            return Ok(position);
        }
    }
}

