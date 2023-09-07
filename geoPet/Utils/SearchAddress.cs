namespace geoPet.Utils
{
    public class SearchAddress
    {
        private readonly HttpClient _httpClient;
        public SearchAddress()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/reverse?format=json&");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> SearchAddressWithLatitudeAndLongitude(string latitude, string longitude)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"lat ={latitude}&lon ={longitude}");

                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    if (conteudo.Contains("error"))
                    {
                        return null;
                    }

                    return conteudo;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
