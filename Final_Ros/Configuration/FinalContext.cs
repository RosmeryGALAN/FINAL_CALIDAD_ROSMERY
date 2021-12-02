using Microsoft.EntityFrameworkCore;
using Final_Ros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Ros.Configuration
{
    public class FinalContext : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Transaccion> Transaccions { get; set; }

        public FinalContext(DbContextOptions<FinalContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new TransaccionMap());
        }
    }
}
