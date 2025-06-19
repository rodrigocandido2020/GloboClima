using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Dominio.Models.Favoritos
{
    [DynamoDBTable("Favoritos")]
    public class Favorito
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Cidade { get; set; }
        public string CodigoPais { get; set; }
        public string NomePais { get; set; }
    }
}
