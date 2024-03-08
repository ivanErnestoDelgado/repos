using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class EventoTests
    {
        const string eventoAntesCristo = "-3114/08/11-- Día a partir del cual se identifican las fechas en el calendario maya Cuenta larga.";
        const string eventoDespuesCristo = "0009/11/17-- Nacimiento de Vespasiano, fundador de la dinastía Flavia.";
        [TestMethod()]
        public void EventoAntesDeCristo()
        {

            var evento = new Evento(eventoAntesCristo);
            Assert.AreEqual(false, evento.sucedioDespuesDeCristo);

        }
        [TestMethod()]
        public void EventoDespuesDeCristo()
        {
            var evento = new Evento(eventoDespuesCristo);
            Assert.AreEqual(true, evento.sucedioDespuesDeCristo);

        }
        [TestMethod()]
        public void lineaCorrupta()
        {
            var evento = new Evento("-3114/08/11-- Día a partir del cual se identifican las fechas en el calendario maya Cuenta larga.");
            Assert.ThrowsException<LineaCorruptaExcepcion>(() => new Evento(""));

        }
        [TestMethod()]
        public void EventoFechaCorrecta()
        {

            var evento1 = new Evento(eventoAntesCristo);
            var fecha1 = new DateTime(3114, 8, 11);
            Assert.AreEqual(evento1.fecha, fecha1);


            var evento2 = new Evento(eventoDespuesCristo);
            var fecha2 = new DateTime(9, 11, 17);
            Assert.AreEqual(evento2.fecha, fecha2);


        }
        [TestMethod()]
        public void EventoFechaIncorrecta()
        {
            //var fecha1 = DateTime();
            Assert.ThrowsException<LineaCorruptaExcepcion>(() => new Evento("1000/02/30-- Sucedio algo increible"));
            Assert.ThrowsException<LineaCorruptaExcepcion>(() => new Evento("+la pura descripcion"));
        }
        [TestMethod()]
        public void DescripcionCorrecta()
        {
            var evento1 = new Evento(eventoAntesCristo);
            Assert.AreEqual("Día a partir del cual se identifican las fechas en el calendario maya Cuenta larga.", evento1.descripcion);


        }
        [TestMethod()]
        public void DescripcionIncorrecta()
        {

            Assert.ThrowsException<LineaCorruptaExcepcion>(() => new Evento("0009/11/17--"));

        }
        [TestMethod]
        public void contarEventos()
        {

            string nombreArchivo = "Prueba.txt";
            MazoEventos mazo = new MazoEventos(nombreArchivo);
            Assert.AreEqual(5, mazo.cantidadEventosTotal);

        }

        [TestMethod]
        public void ArchivoIncorrecto()
        {

            string nombreArchivo = "archivo_invalido.txt";
            Assert.ThrowsException<ArchivoInvalidoExcepcion>(() => new MazoEventos(nombreArchivo));
        }
        [TestMethod]
        public void ArchivoSinEventos()
        {

            string nombreArchivo = "ArchivoVacio.txt";
            Assert.ThrowsException<ArchivoSinEventosExcepcion>(() => new MazoEventos(nombreArchivo));
        }
        [TestMethod]
        public void ChecarMazo_vacio()
        {
            MazoEventos mazo = new MazoEventos("Prueba.txt");
            bool estaVacio = mazo.ChecarMazo(5, 5);
            Assert.IsTrue(estaVacio);
        }

        [TestMethod]
        public void ChecarMazo_NoVacio()
        {
            MazoEventos mazo = new MazoEventos("Prueba.txt");
            bool estaVacio = mazo.ChecarMazo(1, 4);
            Assert.IsFalse(estaVacio);
        }
        [TestMethod]
        public void RepartirEventoRegresaAlgo()
        {

            MazoEventos mazo = new MazoEventos("Prueba.txt");
            Evento evento = mazo.repartirCarta();
            Assert.IsNotNull(evento);
        }
        
        


        



    }
}


