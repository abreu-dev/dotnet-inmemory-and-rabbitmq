using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supply.Domain.Entities;

namespace Supply.Infra.Data.Mappings
{
    public class VeiculoModeloMapping : IEntityTypeConfiguration<VeiculoModelo>
    {
        public void Configure(EntityTypeBuilder<VeiculoModelo> builder)
        {
            builder.ToTable("VeiculoModelo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(x => x.Removed)
                .HasColumnName("Removed");

            builder.HasOne(x => x.VeiculoMarca)
                .WithMany(x => x.VeiculoModelos)
                .IsRequired();
        }
    }
}
