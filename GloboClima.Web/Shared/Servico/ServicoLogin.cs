using System.Threading.Tasks;
using GloboClima.Web.Shared.Servico;
using GloboClima.Web.Shared.ViewModels;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace GloboClima.Web.Shared.Servico
{
    public class ServicoLogin
    {
        private readonly ServicoAPI _api;
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigation;

        public ServicoLogin(ServicoAPI api, IJSRuntime js, NavigationManager navigation)
        {
            _api = api;
            _js = js;
            _navigation = navigation;
        }

        public async Task<bool> UsuarioJaLogado()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task<(bool sucesso, string? erro)> Login(LoginViewModel login)
        {
            var (resultado, problema) = await _api.PostAsync<TokenViewModel>("api/auth", login);

            if (problema != null)
            {
                return (false, problema.Detail);
            }

            if (!string.IsNullOrEmpty(resultado?.Token))
            {
                await _js.InvokeVoidAsync("localStorage.setItem", "authToken", resultado.Token);
                _navigation.NavigateTo("/", forceLoad: true);
                return (true, null);
            }

            return (false, "Token inválido.");
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _navigation.NavigateTo("/login", forceLoad: true);
        }
    }
}
