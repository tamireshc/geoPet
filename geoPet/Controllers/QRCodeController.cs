using geoPet.Services;
using geoPet.Utils;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QRCodeController : ControllerBase
    {
        private OwerService _owerService;
        private PetService _petService;

        public QRCodeController(OwerService service, PetService petService)
        {
            _owerService = service;
            _petService = petService;
        }


        [HttpGet("Pet/{id}")]
        public IActionResult getPetQrCode(int id)
        {
            _petService.findById(id);

            QRCodeGenerator QRCodeGenerator = new QRCodeGenerator();
            var image = QRCodeGenerator.GenerateQrCode("Pet", id.ToString());
            return File(image, "image/jpeg");
        }

        [HttpGet("Ower/{id}")]
        public IActionResult getOwerQrCode(int id)
        {
            _owerService.findById(id);

            QRCodeGenerator QRCodeGenerator = new QRCodeGenerator();
            var image = QRCodeGenerator.GenerateQrCode("Ower", id.ToString());
            return File(image, "image/jpeg");
        }
    }
}
