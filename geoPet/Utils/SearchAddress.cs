using geoPet.Exceptions;
using System.Net.Http.Headers;

namespace geoPet.Utils
{
    public class SearchAddress
    {
        private readonly HttpClient _httpClient;
        public SearchAddress()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> SearchAddressWithLatitudeAndLongitude(string latitude, string longitude)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApp", "1.0"));
                HttpResponseMessage response = await _httpClient.GetAsync($"reverse?format=json&lat={latitude}&lon={longitude}");
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    if (conteudo.Contains("error"))
                    {
                        throw new InvalidValueException("Incorrect values on address request x");
                    }
                    return conteudo;
                }
                throw new InvalidValueException("Incorrect values on address request");
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }
    }
}
