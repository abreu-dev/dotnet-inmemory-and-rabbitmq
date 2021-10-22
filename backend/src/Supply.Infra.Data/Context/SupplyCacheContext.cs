using MongoDB.Driver;
using Supply.Caching.Entities;

namespace Supply.Infra.Data.Context
{
    public class SupplyCacheContext
    {
        private readonly IMongoDatabase Database;

        public SupplyCacheContext()
        {
            var client = new MongoClient("mongodb://abreu:RfAjiY5LL5@localhost:27017/admin");
            Database = client.GetDatabase("Supply");
        }

        public IMongoCollection<VehicleCache> VehiclesCache
        {
            get
            {
                return Database.GetCollection<VehicleCache>("Vehicles");
            }
        }

        public IMongoCollection<VeiculoMarcaCache> VeiculoMarcaCache
        {
            get
            {
                return Database.GetCollection<VeiculoMarcaCache>("VeiculoMarca");
            }
        }
    }
}
