using GloboClima.Dominio.Excecoes;
using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.PaisResponse;
using GloboClima.Dominio.Models.WeatherResponses;

namespace GloboClima.Servico.Servicos
{
    public class ServicoClimaPais : IServicoPaisClima
    {
        private readonly IServicoOpenWeatherMap _servicoOpenWeatherMap;
        private readonly IServicoRestCountries _servicoRestCountries;

        public ServicoClimaPais(
            IServicoOpenWeatherMap servicoOpenWeatherMap,
            IServicoRestCountries servicoRestCountries)
        {
            _servicoOpenWeatherMap = servicoOpenWeatherMap;
            _servicoRestCountries = servicoRestCountries;   
        }

        public async Task<WeatherResponse> ObterClimaCidade(string? cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new BadRequestException("Cidade é obrigatória.");

            var resultado = await _servicoOpenWeatherMap.ObterClimaPorCidade(cidade);

            if (resultado == null)
                throw new NotFoundException("Não foi possível obter o clima para a cidade informada.");

            return resultado;
        }

        public async Task<PaisResponse> ObterDadosPais(string? codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new BadRequestException("Código do país é obrigatório.");

            var resultado = await _servicoRestCountries.ObterDadosPais(codigo);

            if (resultado == null)
                throw new NotFoundException("País não encontrado para o código informado.");

            return resultado;
        }
    }
}
