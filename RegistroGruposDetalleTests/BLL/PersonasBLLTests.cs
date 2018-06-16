using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroGruposDetalle.BLL;
using RegistroGruposDetalle.DAL;
using RegistroGruposDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistroGruposDetalle.BLL.Tests
{
    [TestClass()]
    public class PersonasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Repositorio<Personas> repositorio = new Repositorio<Personas>(new Contexto());
            Personas persona = new Personas
            {
                /*PersonaId = 0
                Nombres = "Lewis",
                Cedula = "059-0004025-7",
                Telefono = "809-555-2828",*/
                Direccion = "Calle principal, castillo"
            };
            bool paso = repositorio.Guardar(persona);
            Assert.AreEqual(true, paso);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.Fail();
        }
    }
}