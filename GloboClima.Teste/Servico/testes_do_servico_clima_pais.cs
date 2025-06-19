using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Excecoes;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using Moq;

namespace GloboClima.Teste.Servico
{
    public class testes_do_servico_clima_pais
    {

        [Fact]
        public async Task ObterClimaCidade_DeveLancarBadRequestException_QuandoCidadeNaoForEnviada()
        {
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();


            var servico = new ServicoClimaPais(
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => servico.ObterClimaCidade(""));
            Assert.Equal("Cidade é obrigatória.", exception.Message);
        }

        [Fact]
        public async Task ObterClimaCidade_DeveLancarNotFoundException_QuandoCidadeNaoForEncontrada()
        {
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();


            var servico = new ServicoClimaPais(
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => servico.ObterClimaCidade("Teste"));
            Assert.Equal("Não foi possível obter o clima para a cidade informada.", exception.Message);
        }


        [Fact]
        public async Task ObterDadosPais_DeveLancarBadRequestException_QuandoCidadeNaoForEnviada()
        {
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();


            var servico = new ServicoClimaPais(
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => servico.ObterDadosPais(""));
            Assert.Equal("Código do país é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task ObterDadosPais_DeveLancarNotFoundException_QuandoPaisNaoForEncontrada()
        {
            var mockClima = new Mock<IServicoOpenWeatherMap>();
            var mockPais = new Mock<IServicoRestCountries>();


            var servico = new ServicoClimaPais(
                mockClima.Object,
                mockPais.Object
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => servico.ObterDadosPais("Teste"));
            Assert.Equal("País não encontrado para o código informado.", exception.Message);
        }
    }
}
