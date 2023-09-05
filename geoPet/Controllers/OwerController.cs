using geoPet.Entities;
using geoPet.Models;
using geoPet.Services;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwerController : ControllerBase
    {
        private OwerService _owerService;
        public OwerController(OwerService service)
        {
            this._owerService = service;
        }

        [HttpPost]
        public IActionResult PostOwer(OwerRequest request)
        {
            var result = _owerService.PostOwer(request);
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.error = result;
            if (result == null)
            {
                return Ok();
            }
            return BadRequest(errorResponse);
        }

        [HttpGet]
        public IActionResult findAll()
        {
            List<OwerResponse> owers = _owerService.findAll();
            return Ok(owers);
        }


        [HttpGet("{id}")]
        public IActionResult findById(int id)
        {
            OwerResponse ower = _owerService.findById(id);
            return Ok(ower);
        }

    }
}
