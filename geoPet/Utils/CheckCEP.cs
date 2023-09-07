using geoPet.Exceptions;
using System.Net.Http;

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
                // Faça uma solicitação GET para a API
                HttpResponseMessage response = await _httpClient.GetAsync(cep + "/json"); // Substitua pelo endpoint correto

                // Verifique se a solicitação foi bem-sucedida (código de status 200)
                if (response.IsSuccessStatusCode)
                {
                    // Leia o conteúdo da resposta como uma string
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
                // Lidar com erros de rede ou falhas na chamada da API
                return $"Erro de HTTP: {e.Message}";
            }
        }

    }

}
