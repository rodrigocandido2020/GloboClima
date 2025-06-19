using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Models.Favoritos;
using GloboClima.Dominio.Excecoes;
using GloboClima.Dominio.Models.WeatherResponses;
using Amazon.DynamoDBv2.DocumentModel;
using GloboClima.Dominio.Interfaces;

namespace GloboClima.Servico.Servicos
{
    public class ServicoFavorito : IServicoFavorito
    {
        private readonly IDynamoDBContext _context;
        private readonly IServicoOpenWeatherMap _servicoOpenWeatherMap;
        private readonly IServicoRestCountries _servicoRestCountries;

        public ServicoFavorito(
            IDynamoDBContext context,
            IServicoOpenWeatherMap servicoOpenWeatherMap,
            IServicoRestCountries servicoRestCountries)
        {
            _context = context;
            _servicoOpenWeatherMap = servicoOpenWeatherMap;
            _servicoRestCountries = servicoRestCountries;   
        }

        public async Task<Favorito> SalvarFavoritos(string? cidade)
        {
            var resultado = await ValidarCidade(cidade);

            var resultadoNomePais = await _servicoRestCountries.ObterPaisPorCodigo(resultado.Sys.Country);

            var favorito = new Favorito
            {
                Id = Guid.NewGuid().ToString(),
                Cidade = cidade,
                CodigoPais = resultado.Sys.Country,
                NomePais = resultadoNomePais!.Translations["por"].Common
            };

            await _context.SaveAsync(favorito);
            
            return favorito;
        }

        private async Task<WeatherResponse> ValidarCidade(string? cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new BadRequestException("O campo Cidade,devem ser preenchido.");

            var resultado = await _servicoOpenWeatherMap.ObterClimaPorCidade(cidade);

            if (resultado == null)
                throw new NotFoundException($"A cidade '{cidade}' é inválida ou não foi encontrada. Não é possível salvar nos favoritos.");

            await VerificarCidadeExistente(cidade);

            return resultado;

        }

        public async Task VerificarCidadeExistente(string cidade)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition(nameof(Favorito.Cidade), ScanOperator.Equal, cidade)
            };

            var favoritos = await _context.ScanAsync<Favorito>(conditions).GetRemainingAsync();

            if (favoritos.Any())
                throw new ConflictException($"A cidade '{cidade}' já está salva nos favoritos.");
        }

        public async Task<List<Favorito>> ListarFavoritos()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<Favorito>(conditions).GetRemainingAsync();
        }

        public async Task DeletarFavoritos(string id)
        {
            var favoritoExistente = await _context.LoadAsync<Favorito>(id);
            if (favoritoExistente == null)
                throw new NotFoundException("Favorito não encontrado para o ID informado.");

            await _context.DeleteAsync(favoritoExistente);
        }
    }
}
