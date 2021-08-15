using Mongo.Domain.Schemas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongo.Data.Interfaces
{
    public interface IRestauranteRepository
    {
        Task<IEnumerable<RestauranteSchema>> Obter();
        Task<RestauranteSchema> ObterPorId(string id);
        Task<IEnumerable<RestauranteSchema>> ObterPorNome(string nome);
        Task<bool> Criar(RestauranteSchema restaurante);
        Task<bool> Editar(string id, RestauranteSchema restaurante);
        Task<bool> Remover(string id);
        Task<bool> Avaliar(string restauranteId, AvaliacaoSchema avaliacao);
        Task<IEnumerable<AvaliacaoSchema>> ObterAvaliacoes(string restauranteId);
        Task<Dictionary<RestauranteSchema, double>> ObterTop3();
    }
}
