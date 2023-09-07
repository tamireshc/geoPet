using geoPet.Utils;

namespace geoPet.Services
{
    public class SearcheAddressService
    {
        public string searchAdress(string latitude, string longitude)
        {
            var searchAddressClass = new SearchAddress();
            var resultSearch = searchAddressClass.SearchAddressWithLatitudeAndLongitude(latitude, longitude);
            return resultSearch.Result;
        }
    }
}
