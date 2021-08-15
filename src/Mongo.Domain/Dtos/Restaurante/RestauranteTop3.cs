namespace Mongo.Domain.Dtos.Restaurante
{
    public class RestauranteTop3
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cozinha { get; set; }
        public string Cidade { get; set; }
        public double Estrelas { get; set; }
    }
}
