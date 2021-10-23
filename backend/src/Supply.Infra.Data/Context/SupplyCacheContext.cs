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

        public IMongoCollection<VeiculoCache> VeiculoCache
        {
            get
            {
                return Database.GetCollection<VeiculoCache>("Veiculo");
            }
        }

        public IMongoCollection<VeiculoMarcaCache> VeiculoMarcaCache
        {
            get
            {
                return Database.GetCollection<VeiculoMarcaCache>("VeiculoMarca");
            }
        }

        public IMongoCollection<VeiculoModeloCache> VeiculoModeloCache
        {
            get
            {
                return Database.GetCollection<VeiculoModeloCache>("VeiculoModelo");
            }
        }
    }
}
