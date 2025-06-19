using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Dominio.Models.Usuarios
{
    [DynamoDBTable("Usuarios")]
    public class Usuario
    {
        [DynamoDBHashKey] // Chave primária
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
