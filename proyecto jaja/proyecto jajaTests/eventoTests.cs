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
        [TestMethod()]
        public void EventoAntesDeCristo()
        {
            var eventos = new Evento("-3114/08/11--  Día a partir del cual se identifican las fechas en el calendario maya Cuenta larga.");
            Assert.AreEqual(true, eventos.sucedioDespuesDeCristo);

            Assert.Fail;
          
        }
        [TestMethod()]
        public void EventoDepuesDeCristo()
        {
            var eventos = new evento("-3114/08/11--  Día a partir del cual se identifican las fechas en el calendario maya Cuenta larga.");
            Assert.AreEqual(true, eventos.sucedioDespuesDeCristo);

            Assert.Fail;

        }
    }
}