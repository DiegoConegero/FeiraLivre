using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeiraLivre.Core.Entities
{
    public class FeiraLivreEntity
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