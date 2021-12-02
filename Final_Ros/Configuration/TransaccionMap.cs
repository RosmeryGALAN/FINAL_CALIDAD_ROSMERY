using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Final_Ros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Ros.Configuration
{
    public class TransaccionMap : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transaccion");
            builder.HasKey(o => o.Id);
        }
    }
}
