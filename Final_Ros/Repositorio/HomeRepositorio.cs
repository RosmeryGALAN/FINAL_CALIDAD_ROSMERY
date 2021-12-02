using Final_Ros.Configuration;
using Final_Ros.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Ros.Repositorio
{
    public interface IHomeRepositorio {
        List<Cuenta> GetCuentas();
        Cuenta GetCuenta(int id);
        List<Transaccion> GetTransaccions(int idCuenta);
        List<Transaccion> GetTransaccionsTipo(int idCuenta, string Tipo);
        void SaveCuentas(Cuenta cuenta);
        void SaveTransaccion(Transaccion transaccion);
    }
    public class HomeRepositorio : IHomeRepositorio
    {
        private FinalContext context;
        public HomeRepositorio(FinalContext context)
        {
            this.context = context;
        }

        public List<Cuenta> GetCuentas()
        {
            return context.Cuentas.ToList();
        }

        public List<Transaccion> GetTransaccions(int idCuenta)
        {
            return context.Transaccions.Where(o => o.IdCuenta == idCuenta).ToList();
        }

        public void SaveCuentas(Cuenta cuenta)
        {
            context.Cuentas.Add(cuenta);
            context.SaveChanges();
        }
        public Cuenta GetCuenta(int id)
        {
            return context.Cuentas.First(o => o.Id == id);
        }
        public void SaveTransaccion(Transaccion transaccion)
        {
            context.Transaccions.Add(transaccion);
            context.SaveChanges();
            ModificaMontoCuenta(transaccion.IdCuenta);
        }
        private void ModificaMontoCuenta(int cuentaId)
        {
            var cuenta = context.Cuentas
                .Include(o => o.Transaccions)
                .FirstOrDefault(o => o.Id == cuentaId);

            var total = cuenta.Transaccions.Sum(o => o.Monto);
            cuenta.Saldo = total;
            context.SaveChanges();
        }

        public List<Transaccion> GetTransaccionsTipo(int idCuenta, string Tipo)
        {
            return context.Transaccions.Where(o => o.IdCuenta == idCuenta && o.Tipo == Tipo).ToList();
        }
    }
}
