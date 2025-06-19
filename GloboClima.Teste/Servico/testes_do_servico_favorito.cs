using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Excecoes;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using GloboClima.Teste.ServicosMock;
using Moq;

namespace GloboClima.Teste.Servico
{
    public class testes_do_servico_favorito
    {

        [Fact]
        public async Task SalvarFavoritos_DeveLancarNotFoundException_QuandoCidadeNaoForEncontrada()
        {
            var mockContext = new Mock<IDynamoDBContext>();
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();

            var servico = new ServicoFavorito(
                mockContext.Object,
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => servico.SalvarFavoritos("Teste"));
            Assert.Equal("A cidade 'Teste' é inválida ou não foi encontrada. Não é possível salvar nos favoritos.", exception.Message);
        }

        [Fact]
        public async Task SalvarFavoritos_DeveLancarBadRequestException_QuandoCidadeNaoForEnviada()
        {
            var mockContext = new Mock<IDynamoDBContext>();
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();

            var servico = new ServicoFavorito(
                mockContext.Object,
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => servico.SalvarFavoritos(""));
            Assert.Equal("O campo Cidade,devem ser preenchido.", exception.Message);
        }
    }
}
