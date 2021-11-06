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

        public string Registrar(string c, string p)
        {
            bool temp = datos.Registrar(c, p);

            if (temp == true)
            {
                return "User registered successfully";
            }

            return "That e-mail is already registered!";
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


        public bool Matricular(string correo, string sN, string sL1, string sL2, long id, byte idT, string sNE, string sLE1, string sLE2, long idE, byte idTE, string procedence, int gen, int genE, int grado, int telf)
        {
            return datos.Matricular(correo, sN, sL1, sL2, id, idT, sNE, sLE1, sLE2, idE, idTE, procedence, gen, genE, grado, telf);
        }

        public string EnrolledCurrentYear(string c)
        {
            return datos.EnrolledCurrentYear(c);
        }

        public string LlenarDatos(string c)
        {
            string temp = "";
            List<LlenarDatos_Result> list = datos.LlenarDatos(c);

            temp = "Student Enroll Summary:<br />" +
                "Nombre: " + list[0].Nombre + "<br />" +
                "First last name: " + list[0].PrimerApellido + "<br />" +
                "Second last name: " + list[0].SegundoApellido + "<br />" +
                "ID type: " + list[0].Descripcion + "<br />" +
                "ID Value: " + list[0].Valor + "<br />" +
                "School of procedence: " + list[0].escuela + "<br />" +
                "Email of reference: " + list[1].contacto + "<br />" +
                "Phone of reference: " + list[0].contacto + "<br /><br />" + 
                "Enrollment Status: " + list[0].estado;

            return temp;
        }

    }
}