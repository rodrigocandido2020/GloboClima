using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using GloboClima.Dominio.Models.Favoritos;

namespace GloboClima.Servico.Servicos
{
    public class ServicoFavorito
    {
        private readonly DynamoDBContext _context;

        public ServicoFavorito(IAmazonDynamoDB dynamoDb)
        {
            _context = new DynamoDBContext(dynamoDb);
        }

        public async Task SalvarCidadeFavoritaAsync(Favorito favorito)
        {
            await _context.SaveAsync(favorito);
        }

        public async Task<List<Favorito>> ListarFavorita()
        {
            var conditions = new List<ScanCondition>(); // Nenhum filtro = scan completo
            return await _context.ScanAsync<Favorito>(conditions).GetRemainingAsync();
        }

        public async Task<bool> DeletarFavorita(string id)
        {
            var favoritoExistente = await _context.LoadAsync<Favorito>(id);
            if (favoritoExistente == null)
                return false;

            await _context.DeleteAsync(favoritoExistente);
            return true;
        }

    }
}
