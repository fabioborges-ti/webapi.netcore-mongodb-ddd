using Mongo.Domain.Dtos.Restaurante;
using Mongo.Domain.Schemas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongo.Service.Interfaces
{
    public interface IRestauranteService
    {
        Task<IEnumerable<RestauranteSchema>> Obter();
        Task<RestauranteSchema> ObterPorId(string id);
        Task<IEnumerable<RestauranteSchema>> ObterPorNome(string nome);
        Task<object> Criar(RestauranteDtoCreate obj);
        Task<bool> Editar(string id, RestauranteDtoUpdate obj);
        Task<bool> Remover(string id);
        Task<bool> Avaliar(string restauranteId, AvaliacaoSchema avaliacao);
        Task<IEnumerable<AvaliacaoSchema>> ObterAvaliacoes(string restauranteId);
        Task<IEnumerable<RestauranteTop3>> ObterTop3();
    }
}
