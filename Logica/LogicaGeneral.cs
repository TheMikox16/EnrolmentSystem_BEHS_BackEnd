using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnrolmentSystem_BEHS.Datos;

namespace EnrolmentSystem_BEHS.Logica
{
    public class LogicaGeneral
    {
        private CapaDatos datos = new CapaDatos();

        public int TitleDate()
        {
            int temp = datos.TitleDate();
            return temp;
        }
    }
}