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
        public IActionResult PostOwer(OwerRequest request)
        {
            _owerService.PostOwer(request);
            return Ok();
        }

    }
}
