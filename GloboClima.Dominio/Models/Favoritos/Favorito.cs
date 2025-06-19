using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Dominio.Models.Favoritos
{
    [DynamoDBTable("Favoritos")]
    public class Favorito
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}
