using geoPet.Entities;
using geoPet.Models;
using geoPet.Services;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly PetService _petService;

        public PetController(PetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public IActionResult post(PetRequest request)
        {
            _petService.post(request);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult findById(int id)
        {
            Pet pet = _petService.findById(id);
            return Ok(pet);
        }

        [HttpGet]
        public IActionResult findAll()
        {
            List<Pet> pets = _petService.findAll();
            return Ok(pets);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            _petService.delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult update(int id, PetRequest request)
        {
            _petService.update(id, request);
            return Ok();
        }
    }
}


