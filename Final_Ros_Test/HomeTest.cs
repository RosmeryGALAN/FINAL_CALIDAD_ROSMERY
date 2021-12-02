using Final_Ros.Controllers;
using Final_Ros.Models;
using Final_Ros.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Ros_Test
{
    [TestFixture]
    class HomeTest
    {
        [Test]
        public void IndexTest()
        {
            var repo = new Mock<IHomeRepositorio>();
            var controller = new HomeController(repo.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [Test]
        public void Cuentas()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());

            var controller = new HomeController(repo.Object);
            var view = controller.Cuentas() as ViewResult;

            Assert.AreEqual("Cuentas", view.ViewName);
        }

        [Test]
        public void Crear()
        {
            var repo = new Mock<IHomeRepositorio>();

            var controller = new HomeController(repo.Object);
            var view = controller.Crear() as ViewResult;

            Assert.AreEqual("Crear", view.ViewName);
        }

        [Test]
        public void CrearPost()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.SaveCuentas(new Cuenta()));

            var controller = new HomeController(repo.Object);
            var view = controller.Crear() as ViewResult;

            Assert.AreEqual("Crear", view.ViewName);
        }

        [Test]
        public void Ingresos()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetTransaccionsTipo(5, "Ingreso"));
            repo.Setup(o => o.GetCuenta(5));

            var controller = new HomeController(repo.Object);
            var view = controller.Ingresos(5) as ViewResult;

            Assert.AreEqual("Ingresos", view.ViewName);
        }
        [Test]
        public void Gastos()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetTransaccionsTipo(5, "Gasto"));
            repo.Setup(o => o.GetCuenta(5));

            var controller = new HomeController(repo.Object);
            var view = controller.Gastos(5) as ViewResult;

            Assert.AreEqual("Gastos", view.ViewName);
        }
        [Test]
        public void CrearIngreso()
        {
            var repo = new Mock<IHomeRepositorio>();

            var controller = new HomeController(repo.Object);
            var view = controller.CrearIngreso(5) as ViewResult;

            Assert.AreEqual("CrearIngreso", view.ViewName);
        }
        [Test]
        public void CrearIngresoPost()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetCuenta(5)).Returns(new Cuenta());
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new HomeController(repo.Object);
            var view = controller.CrearIngreso(new Transaccion()) as RedirectToActionResult;

            Assert.AreEqual("Cuentas", view.ActionName);
        }
        [Test]
        public void CrearGasto()
        {
            var repo = new Mock<IHomeRepositorio>();

            var controller = new HomeController(repo.Object);
            var view = controller.CrearGasto(5) as ViewResult;

            Assert.AreEqual("CrearGasto", view.ViewName);
        }
        [Test]
        public void CrearGastoPost()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetCuenta(5)).Returns(new Cuenta() { Id = 5, Limite = 10, Saldo = 10 });
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new HomeController(repo.Object);
            var view = controller.CrearGasto(new Transaccion() { IdCuenta = 5, Tipo = "Gasto", Monto = 10 }) as RedirectToActionResult;

            Assert.AreEqual("Cuentas", view.ActionName);
        }

        [Test]
        public void CrearGastoPostMayor()
        {
            var repo = new Mock<IHomeRepositorio>();
            repo.Setup(o => o.GetCuenta(5)).Returns(new Cuenta() { Id = 5, Limite = 10, Saldo = 10 });
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new HomeController(repo.Object);
            var view = controller.CrearGasto(new Transaccion() { IdCuenta = 5, Tipo = "Gasto", Monto = 500 }) as ViewResult;

            Assert.AreEqual("CrearGasto", view.ViewName);
        }
    }
}