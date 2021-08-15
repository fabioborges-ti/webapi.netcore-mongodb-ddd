using Mongo.Domain.Enums;

namespace Mongo.Domain.Dtos.Restaurante
{
    public class RestauranteDtoCreate
    {
        public string Nome { get; set; }
        public CozinhaEnum Cozinha { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
    }
}
