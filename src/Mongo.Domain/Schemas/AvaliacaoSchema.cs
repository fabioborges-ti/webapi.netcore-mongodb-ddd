using System.Text.Json.Serialization;

namespace Mongo.Domain.Schemas
{
    public class AvaliacaoSchema
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public string RestauranteId { get; set; }
        
        public int Estrelas { get; set; }
        public string Comentario { get; set; }
    }
}
