using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Dni).HasMaxLength(20);
        builder.HasIndex(e => e.Dni).IsUnique();
    }
}
