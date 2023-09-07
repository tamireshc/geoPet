using geoPet.Entities;
using geoPet.Services;
using Microsoft.AspNetCore.Mvc;

namespace geoPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchAddressController : ControllerBase
    {
        private SearcheAddressService _searchAdressService;

        public SearchAddressController(SearcheAddressService searchAdressService)
        {
            _searchAdressService = searchAdressService;
        }

        [HttpGet]
        public IActionResult getAdresss(LatitudeLongitudeRequest request)
        {
            string adresss = _searchAdressService.searchAdress(request.Latitude, request.Longitude);
            return Ok(adresss);
        }

    }
}
