using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Data;
using Supply.Domain.Entities;
using Supply.Infra.Data.Mappings;

namespace Supply.Infra.Data.Context
{
    public class SupplyDataContext : BaseDbContext
    {
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<VeiculoMarca> VeiculoMarca { get; set; }
        public DbSet<VeiculoModelo> VeiculoModelo { get; set; }

        public SupplyDataContext(DbContextOptions<SupplyDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VeiculoMapping());
            modelBuilder.ApplyConfiguration(new VeiculoMarcaMapping());
            modelBuilder.ApplyConfiguration(new VeiculoModeloMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
