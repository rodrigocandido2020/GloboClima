using System.Net.Http.Json;
using System.Text.Json;
using GloboClima.Web.Shared.ProblemDetails;

namespace GloboClima.Web.Shared.Servico
{
    public class servicoAPI
    {
        private readonly HttpClient _http;

        public servicoAPI(HttpClient http)
        {
            _http = http;
        }

        public Task<(T? Resultado, ProblemDetail? Erro)> GetAsync<T>(string url) =>
            RequestAsync<T>(HttpMethod.Get, url);

        public Task<(T? Resultado, ProblemDetail? Erro)> PostAsync<T>(string url, object? body) =>
            RequestAsync<T>(HttpMethod.Post, url, body);

        public async Task<(bool Sucesso, ProblemDetail? Erro)> DeleteAsync(string url)
        {
            var (ok, erro) = await RequestAsync<object>(HttpMethod.Delete, url);
            return (erro == null, erro);
        }

        private async Task<(T? Resultado, ProblemDetail? Erro)> RequestAsync<T>(HttpMethod method, string url, object? body = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);

                if (body != null)
                    request.Content = JsonContent.Create(body);

                var response = await _http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return (await response.Content.ReadFromJsonAsync<T>(), null);

                var json = await response.Content.ReadAsStringAsync();
                var erro = JsonSerializer.Deserialize<ProblemDetail>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                           ?? new ProblemDetail { Detail = "Erro desconhecido." };

                return (default, erro);
            }
            catch (Exception ex)
            {
                return (default, new ProblemDetail { Title = "Erro de comunicação", Detail = ex.Message });
            }
        }
    }
}
