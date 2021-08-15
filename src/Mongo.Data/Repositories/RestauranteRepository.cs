using Mongo.Data.Interfaces;
using Mongo.Data.IoC;
using Mongo.Domain.Schemas;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Data.Repositories
{
    public class RestauranteRepository : IRestauranteRepository
    {
        private readonly IMongoCollection<RestauranteSchema> _restaurantes;
        private readonly IMongoCollection<AvaliacaoSchema> _avaliacoes;

        public RestauranteRepository()
        {
            _restaurantes = MongoDbSettings.Db.GetCollection<RestauranteSchema>("restaurantes");
            _avaliacoes = MongoDbSettings.Db.GetCollection<AvaliacaoSchema>("avaliacoes");
        }

        public Task<bool> Criar(RestauranteSchema restaurante)
        {
            _restaurantes.InsertOne(restaurante);

            return Task.FromResult(true);
        }

        public async Task<bool> Editar(string id, RestauranteSchema restaurante)
        {
            var result = await _restaurantes.ReplaceOneAsync(item => item.Id.Equals(id), restaurante);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> Remover(string id)
        {
            await _avaliacoes.DeleteManyAsync(item => item.Id.Equals(id));

            await _restaurantes.DeleteOneAsync(item => item.Id.Equals(id));

            return true;
        }

        public async Task<IEnumerable<RestauranteSchema>> Obter()
        {
            var restaurantes = new List<RestauranteSchema>();

            await _restaurantes
                    .AsQueryable()
                    .ForEachAsync(item =>
                    {
                        restaurantes.Add(item);
                    });
            
            return restaurantes;
        }

        public async Task<RestauranteSchema> ObterPorId(string id)
        {
            return await _restaurantes.AsQueryable().FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IEnumerable<RestauranteSchema>> ObterPorNome(string nome)
        {
            var restaurantes = new List<RestauranteSchema>();

            await _restaurantes
                    .AsQueryable()
                    .Where(item => item.Nome.ToLower().Contains(nome.ToLower()))
                    .ForEachAsync(item => {
                        restaurantes.Add(item);
                    });

            return restaurantes;
        }

        public Task<bool> Avaliar(string restauranteId, AvaliacaoSchema avaliacao)
        {
            var nota = new AvaliacaoSchema
            {
                RestauranteId = restauranteId,
                Estrelas = avaliacao.Estrelas,
                Comentario = avaliacao.Comentario
            };

            _avaliacoes.InsertOne(nota);

            return Task.FromResult(true);
        }

        public async Task<IEnumerable<AvaliacaoSchema>> ObterAvaliacoes(string restauranteId)
        {
            return await _avaliacoes.AsQueryable().Where(item => item.RestauranteId.Equals(restauranteId)).ToListAsync();
        }

        public async Task<Dictionary<RestauranteSchema, double>> ObterTop3()
        {
            var result = new Dictionary<RestauranteSchema, double>();

            var top3 = _avaliacoes
                .Aggregate()
                .Group(id => id.RestauranteId,
                    group => new { RestauranteId = group.Key, MediaEstrelas = group.Average(a => a.Estrelas) })
                .SortByDescending(sort => sort.MediaEstrelas)
                .Limit(3);

            await top3.ForEachAsync(item =>
            {
                var restaurante = ObterPorId(item.RestauranteId).Result;

                result.Add(restaurante, item.MediaEstrelas);

            });

            return result;
        }
    }
}
