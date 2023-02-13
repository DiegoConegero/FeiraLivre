using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FeiraLivre.AtualizarBanco
{
    public class FeiraLivre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Bairro { get; set; }

        public string Regiao { get; set; }

        public string Distrito { get; set; }
    }
}