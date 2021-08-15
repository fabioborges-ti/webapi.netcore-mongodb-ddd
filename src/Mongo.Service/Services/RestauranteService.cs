using Mongo.Data.Interfaces;
using Mongo.Domain.Dtos.Restaurante;
using Mongo.Domain.Schemas;
using Mongo.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Service.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly IRestauranteRepository _repository;

        public RestauranteService(IRestauranteRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Criar(RestauranteDtoCreate obj)
        {
            var restaurante = new RestauranteSchema
            {
                Id = Guid.NewGuid().ToString(),
                Nome = obj.Nome,
                TipoCozinha = (int) obj.Cozinha,
                Cozinha = obj.Cozinha.ToString(),
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Cidade = obj.Cidade,
                Uf = obj.Uf,
                Cep = obj.Cep
            };

            return await _repository.Criar(restaurante);
        }

        public async Task<bool> Editar(string id, RestauranteDtoUpdate obj)
        {
            var data = await _repository.ObterPorId(id);

            if (data == null) return false;

            var restaurante = new RestauranteSchema
            {
                Id = obj.Id,
                Nome = obj.Nome,
                TipoCozinha = (int) obj.Cozinha,
                Cozinha = obj.Cozinha.ToString(),
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Cidade = obj.Cidade,
                Uf = obj.Uf,
                Cep = obj.Cep
            };
            
            var result = await _repository.Editar(id, restaurante);

            return result;
        }

        public async Task<bool> Remover(string id)
        {
            var data = await _repository.ObterPorId(id);

            if (data == null) return false;

            return await _repository.Remover(id);
        }

        public async Task<bool> Avaliar(string restauranteId, AvaliacaoSchema avaliacao)
        {
            var data = _repository.ObterPorId(restauranteId);

            if (data == null) return false;

            return await _repository.Avaliar(restauranteId, avaliacao);
        }

        public async Task<IEnumerable<RestauranteSchema>> Obter()
        {
            return await _repository.Obter();
        }

        public async Task<RestauranteSchema> ObterPorId(string id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task<IEnumerable<RestauranteSchema>> ObterPorNome(string nome)
        {
            var result = await _repository.ObterPorNome(nome);

            return result;
        }

        public async Task<IEnumerable<AvaliacaoSchema>> ObterAvaliacoes(string restauranteId)
        {
            return await _repository.ObterAvaliacoes(restauranteId);
        }

        public async Task<IEnumerable<RestauranteTop3>> ObterTop3()
        {
            var top3 = await _repository.ObterTop3();

            var result = top3.Select(item => 
                new RestauranteTop3 
                {
                    Id = item.Key.Id,
                    Nome = item.Key.Nome,
                    Cozinha = item.Key.Cozinha.ToString(),
                    Cidade = item.Key.Cidade,
                    Estrelas = item.Value
                });

            return result;
        }
    }
}
