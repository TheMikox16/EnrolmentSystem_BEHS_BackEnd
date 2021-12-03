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

        /**
         * Metodo para ingresar al sistema. Es una llamada a la capa de datos 
         * 
        */
        public List<Usuarios> Ingresar (string c, string p)
        {
            return datos.Ingresar(c, p);
        }

        /**
         * Metodo para registrar un nuevo usuario. Verifica que las contraseñas sean correctas y cumplan
         * con ciertos criterios de seguridad. Asi mismo, verifica los valores numericos y si el correo
         * digitado esta o no registrado.
        */
        public string Registrar(string c, string p, string co, string u, string pho)
        {
            
            if (!VerificarCorreo(c))
            {
                return "Email sintaxis is not allowed, please write a valid email address";
            }

            if (!p.Equals(co))
            {
                return "Passwords doesn't match, try again";
            }

            if (!VerificarTelefono(pho))
            {
                return "Phone Number has a invalid value, please try with a different value";
            }

            long phoneNumb = Int64.Parse(pho);

            bool temp = datos.Registrar(c, p, u, phoneNumb);

            if (temp == true)
            {
                return "User registered successfully";
            }

            return "That e-mail is already registered!";
        }

        /**
         * Metodo auxiliar que valida un correo según ciertos parametros
         * 
         */
        public bool VerificarCorreo(string s)
        {
            foreach(char c in s)
            {
                if (c.Equals('@') && s.ToCharArray()[s.Length-1] != c)
                {
                    if (Char.IsLetter(s[s.IndexOf('@') + 1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * Metodo que intenta parsear el valor numerico del telefono. Si falla, regresa un false, si tiene exito, regresa true
         */ 
        public bool VerificarTelefono(string s)
        {
            bool temp = Int64.TryParse(s,out long result);
            return temp;
        }

        /**
         * Metodo que realiza la matricula por medio de una llamada a la base de datos. Se utiliza todos los datos introducidos
         * en el formulario de Matricular.aspx
         */
        public bool Matricular(string correo, string sN, string sL1, string sL2, long id, byte idT, string sNE, string sLE1, string sLE2, long idE, byte idTE, string procedence, int gen, int genE, int grado, int telf, byte[] doc)
        {
            return datos.Matricular(correo, sN, sL1, sL2, id, idT, sNE, sLE1, sLE2, idE, idTE, procedence, gen, genE, grado, telf, doc);
        }


        /**
         * Metodo que determina si un estudiante ya se ha matriculado en ese año o si necesitara matricularse nuevamente 
         * ya que es un nuevo año 
         */
        public string EnrolledCurrentYear(string c)
        {
            return datos.EnrolledCurrentYear(c);
        }

        /**
         * Metodo encargado de llenar los datos enviados al proceso de matricula. Consulta a la base de datos por
         * medio de la capa de datos y contruye el string a ser desplegado por medio de la consulta.
         */
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

        /**
         * Metodo encargado de conectarse con la capa de datos para realizar la correcta actualización de los datos del usuario
         */
        public string ActualizarUsuario(string c, string n, string pho)
        {
            if (!VerificarTelefono(pho))
            {
                return "Phone Number has a invalid value, please try with a different value";
            }

            long phoneNumb = Int64.Parse(pho);

            bool temp = datos.ActualizarUsuario(c, n, phoneNumb);

            if (temp)
            {
                return "User information updated successfully";
            }
            else
            {
                return "Fail updating user information!!";
            }

        }

        public string[] LlenarInformacion(string c)
        {
            Usuarios u = datos.LlenarInformacion(c);

            string[] temp = new string[8];
            temp[0] = u.correo.ToString();
            temp[1] = u.nombre.ToString();
            temp[2] = u.telefono.ToString();

            return temp;

        }

        public string ActualizarContra(string c, string o, string n, string con)
        {
            if (o.Equals(n))
            {
                return "The new password must not be the same as the current one";
            }

            if (!n.Equals(con))
            {
                return "The new password does not match with confirmation, try again";
            }

            List<Usuarios> l = datos.Ingresar(c, o);

            if(l.Count() <= 0)
            {
                return "Your current password does not match, try again";
            }

            bool t = datos.ActualizarContra(c, n);

            if (t)
            {
                return "Password updated successfully";
            }

            return "Failed to change password!!";

        }

    }
}