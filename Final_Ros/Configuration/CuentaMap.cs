using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Final_Ros.Models;

namespace Final_Ros.Configuration
{
    public class CuentaMap : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta");
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Transaccions).
                WithOne().
                HasForeignKey(o => o.IdCuenta);
        }
    }
}
