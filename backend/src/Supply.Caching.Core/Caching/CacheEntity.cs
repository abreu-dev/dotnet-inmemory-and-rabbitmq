using MongoDB.Bson.Serialization.Attributes;

namespace Supply.Caching.Core.Caching
{
    public class CacheEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}
