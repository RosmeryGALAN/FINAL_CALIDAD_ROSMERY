using Final_Ros.Models;
using Final_Ros.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Ros.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepositorio context;
        public HomeController(IHomeRepositorio context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult Cuentas()
        {
            var cuentas = context.GetCuentas();
            ViewBag.MontoTotal = cuentas.Sum(o => o.Saldo);
            return View("Cuentas", cuentas);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.Categorias = new List<string> { "Propio", "Credito" };
            return View("Crear", new Cuenta());
        }

        [HttpPost]
        public ActionResult Crear(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                if (cuenta.Categoria == "Credito")
                {
                    cuenta.Limite = cuenta.Saldo;
                    cuenta.Saldo = 0;
                }
                else
                {
                    cuenta.Limite = 0;
                    cuenta.Transaccions = new List<Transaccion>
                    {
                        new Transaccion
                        {
                            Fecha = DateTime.Now,
                            Tipo = "Ingreso",
                            Monto = cuenta.Saldo,
                            Descripcion = "Saldo Inicial"
                        }
                    };
                }
                context.SaveCuentas(cuenta);
                return RedirectToAction("Cuentas");
            }
            ViewBag.Categorias = new List<string> { "Propio", "Credito" };
            return View("Crear", cuenta);
        }

        [HttpGet]
        public ActionResult Ingresos(int id)
        {
            var transacciones = context.GetTransaccionsTipo(id, "Ingreso");
            ViewBag.Cuenta = context.GetCuenta(id);
            return View("Ingresos", transacciones);
        }

        [HttpGet]
        public ActionResult Gastos(int id)
        {
            var transacciones = context.GetTransaccionsTipo(id, "Gasto");
            ViewBag.Cuenta = context.GetCuenta(id);
            return View("Gastos", transacciones);
        }

        [HttpGet]
        public ActionResult CrearIngreso(int id)
        {
            ViewBag.CuentaId = id;
            return View("CrearIngreso");
        }

        [HttpPost]
        public ActionResult CrearIngreso(Transaccion transaccion)
        {
            var cuenta = context.GetCuenta(transaccion.IdCuenta);
            transaccion.Tipo = "Ingreso";
            transaccion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                context.SaveTransaccion(transaccion);
                return RedirectToAction("Cuentas", new { id = transaccion.IdCuenta });
            }
            ViewBag.CuentaId = transaccion.IdCuenta;
            return View("CrearIngreso");
        }

        [HttpGet]
        public ActionResult CrearGasto(int id)
        {
            ViewBag.CuentaId = id;
            return View("CrearGasto");
        }

        [HttpPost]
        public ActionResult CrearGasto(Transaccion transaccion)
        {
            var cuenta = context.GetCuenta(transaccion.IdCuenta);
            if ((cuenta.Limite + cuenta.Saldo) <= transaccion.Monto)
                ModelState.AddModelError("Cuenta", "El gasto supera el saldo de la cuenta");
            transaccion.Tipo = "Gasto";
            transaccion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                transaccion.Monto *= -1;
                context.SaveTransaccion(transaccion);
                return RedirectToAction("Cuentas", new { id = transaccion.IdCuenta });
            }
            ViewBag.CuentaId = transaccion.IdCuenta;
            return View("CrearGasto");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
