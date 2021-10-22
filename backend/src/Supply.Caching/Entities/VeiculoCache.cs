﻿using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoCache
    {
        [BsonId]
        public string Id { get; set; }

        public string Placa { get; set; }

        public VeiculoCache(Guid id, string placa)
        {
            Id = id.ToString();
            Placa = placa;
        }
    }
}