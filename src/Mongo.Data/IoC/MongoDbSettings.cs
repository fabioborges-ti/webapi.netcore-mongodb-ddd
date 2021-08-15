using Microsoft.Extensions.Configuration;
using Mongo.Domain.Schemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace Mongo.Data.IoC
{
    public static class MongoDbSettings
    {
        public static IMongoDatabase Db { get; set; }

        public static IConfiguration ConfigureDbSettings(this IConfiguration configuration, string connectionString, string databaseName)
        {
            try
            {
                var client = new MongoClient(connectionString);

                Db = client.GetDatabase(databaseName);

                MapSchemas();
            }
            catch (Exception ex)
            {
                throw new MongoException("Ocorreu erro ao tentar se conectar ao banco de dados.", ex);
            }

            return configuration;
        }

        private static void MapSchemas()
        {
            // RestauranteSchema
            if (!BsonClassMap.IsClassMapRegistered(typeof(RestauranteSchema)))
            {
                BsonClassMap.RegisterClassMap<RestauranteSchema>(schema =>
                {
                    schema.AutoMap();
                    schema.SetIdMember(schema.GetMemberMap(c => c.Id));
                    schema.GetMemberMap(c => c.Id).SetElementName("_id");

                    schema.SetIgnoreExtraElements(true);
                });
            }

            // AvaliacaoSchema
            if (!BsonClassMap.IsClassMapRegistered(typeof(AvaliacaoSchema)))
            {
                BsonClassMap.RegisterClassMap<AvaliacaoSchema>(schema =>
                {
                    schema.AutoMap();
                    
                    schema
                        .MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));

                    schema.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
