using geoPet.Entities;
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
        public IActionResult post(OwerRequest request)
        {
            var result = _owerService.post(request);
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

        [HttpDelete("{id}")]
        public IActionResult deleteById(int id)
        {
            _owerService.delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OwerRequest request)
        {
            _owerService.findById(id);
            var result = _owerService.update(id, request);
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.error = result;
            if (result == null)
            {
                return Ok();
            }
            return BadRequest(errorResponse);
        }

        [HttpPost("/Login")]
        public IActionResult login(LoginRequest loginRequest)
        {
            var result = _owerService.login(loginRequest);
            return Ok(result);
        }
    }
}
