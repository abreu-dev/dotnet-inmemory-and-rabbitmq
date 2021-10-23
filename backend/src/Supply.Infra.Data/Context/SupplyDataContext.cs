using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Infra.Data.Mappings;
using System.Threading.Tasks;

namespace Supply.Infra.Data.Context
{
    public class SupplyDataContext : DbContext, IUnitOfWork
    {
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<VeiculoMarca> VeiculoMarca { get; set; }
        public DbSet<VeiculoModelo> VeiculoModelo { get; set; }

        public SupplyDataContext(DbContextOptions<SupplyDataContext> options) : base(options) { }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VeiculoMapping());
            modelBuilder.ApplyConfiguration(new VeiculoMarcaMapping());
            modelBuilder.ApplyConfiguration(new VeiculoModeloMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
