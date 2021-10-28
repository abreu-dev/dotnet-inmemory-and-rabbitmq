using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supply.Domain.Entities;

namespace Supply.Infra.Data.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(x => x.Placa)
                .HasColumnName("Placa")
                .IsRequired();

            builder.Property(x => x.Removed)
                .HasColumnName("Removed");

            builder.HasOne(x => x.VeiculoModelo)
                .WithMany(x => x.Veiculos)
                .IsRequired();
        }
    }
}
