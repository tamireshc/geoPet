using geoPet.Exceptions;
using geoPet.Utils;
using Microsoft.EntityFrameworkCore;


namespace geoPet.Services
{
    public class SearcheAddressService
    {
        public string searchAdress(string latitude, string longitude)
        {
            try
            {
                var searchAddressClass = new SearchAddress();
                var resultSearch = searchAddressClass.SearchAddressWithLatitudeAndLongitude(latitude, longitude);
                if (resultSearch == null)
                { 
                    return "Invalid values";
                }
                else
                {
                    return resultSearch.Result;
                }

            }
            catch (Exception e)
            {
                throw new DuplicatedValueException("Invalid values");
            }

        }
    }
}
