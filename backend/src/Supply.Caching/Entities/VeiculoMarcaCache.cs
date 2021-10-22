using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoMarcaCache
    {
        [BsonId]
        public string Id { get; set; }

        public string Nome { get; set; }

        public VeiculoMarcaCache(Guid id, string nome)
        {
            Id = id.ToString();
            Nome = nome;
        }
    }
}
