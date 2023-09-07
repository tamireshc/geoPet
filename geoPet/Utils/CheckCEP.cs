using geoPet.Exceptions;

namespace geoPet.Utils
{
    public class CheckCEP
    {
        private readonly HttpClient _httpClient;
        public CheckCEP()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> CheckAsyncCEP(String cep)
        {
            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync(cep + "/json");
                if (response.IsSuccessStatusCode)
                {

                    string conteudo = await response.Content.ReadAsStringAsync();
                    if (conteudo.Contains("true"))
                    {
                        throw new InvalidCEPException("Nonexistent CEP");
                    }

                    return conteudo;
                }
                else
                {
                    throw new InvalidCEPException("Invalid CEP");
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erro de HTTP: {e.Message}";
            }
        }

    }

}
