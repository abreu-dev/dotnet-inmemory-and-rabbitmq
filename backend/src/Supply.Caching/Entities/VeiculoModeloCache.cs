using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoModeloCache
    {
        [BsonId]
        public string Id { get; set; }

        public string Nome { get; set; }

        public VeiculoMarcaCache VeiculoMarca { get; set; }

        public VeiculoModeloCache(Guid id, string nome, Guid veiculoMarcaId, string veiculoMarcaNome)
        {
            Id = id.ToString();
            Nome = nome;
            VeiculoMarca = new VeiculoMarcaCache(veiculoMarcaId, veiculoMarcaNome);
        }
    }
}
