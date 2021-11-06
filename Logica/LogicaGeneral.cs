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

        public List<Usuarios> Ingresar (string c, string p)
        {
            return datos.Ingresar(c, p);
        }

        public void Registrar(string c, string p)
        {
            datos.Registrar(c, p);
        }

        public bool VerificarCorreo(string s)
        {
            foreach(char c in s)
            {
                if (c.Equals('@') && s.ToCharArray()[s.Length-1] != c)
                {
                    return true;
                }
            }
            return false;
        }

    }
}