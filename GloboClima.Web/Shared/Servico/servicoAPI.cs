using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GloboClima.Web.Shared.ProblemDetails;
using Microsoft.JSInterop;

namespace GloboClima.Web.Shared.Servico
{
    public class servicoAPI
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;

        public servicoAPI(
            HttpClient http,
            IJSRuntime jsRuntime
            )
        {
            _http = http;
            _jsRuntime = jsRuntime;
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

                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent || typeof(T) == typeof(object))
                        return (default, null);

                    return (await response.Content.ReadFromJsonAsync<T>(), null);
                }

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
