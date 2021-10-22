using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supply.Domain.Entities;

namespace Supply.Infra.Data.Mappings
{
    public class VeiculoMarcaMapping : IEntityTypeConfiguration<VeiculoMarca>
    {
        public void Configure(EntityTypeBuilder<VeiculoMarca> builder)
        {
            builder.ToTable("VeiculoMarca");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(x => x.Removed)
                .HasColumnName("Removed");
        }
    }
}
