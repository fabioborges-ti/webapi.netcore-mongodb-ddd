using Mongo.Domain.Enums;

namespace Mongo.Domain.Dtos.Restaurante
{
    public class RestauranteDtoUpdate
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public CozinhaEnum Cozinha { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
    }
}
