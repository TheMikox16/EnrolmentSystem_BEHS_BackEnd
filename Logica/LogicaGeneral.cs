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
        public List<Usuarios> Ingresar(string c, string p)
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

            if (!VerificarLong(pho))
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

        public string RegistrarAdmin(string c, string p, string co, string u, string pho, int i)
        {

            if (!VerificarCorreo(c))
            {
                return "Email sintaxis is not allowed, please write a valid email address";
            }

            if (!p.Equals(co))
            {
                return "Passwords doesn't match, try again";
            }

            if (!VerificarLong(pho))
            {
                return "Phone Number has a invalid value, please try with a different value";
            }

            long phoneNumb = Int64.Parse(pho);
            byte ii = Byte.Parse(i + "");

            bool temp = datos.RegistrarAdmin(c, p, u, phoneNumb, ii);

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
            foreach (char c in s)
            {
                if (c.Equals('@') && s.ToCharArray()[s.Length - 1] != c)
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
        public bool VerificarLong(string s)
        {
            bool temp = Int64.TryParse(s, out long result);
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


        public string Enrolled(string c)
        {
            return datos.Enrolled(c);
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
            if (!VerificarLong(pho))
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

            if (l.Count() <= 0)
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

        public string ActualizarContra2(string c, string n, string con)
        {

            if (!n.Equals(con))
            {
                return "The new password does not match with confirmation, try again";
            }

            bool t = datos.ActualizarContra(c, n);

            if (t)
            {
                return "Password updated successfully";
            }

            return "Failed to change password!!";

        }

        public List<Object[]> ListarMatricula()
        {
            List<Object[]> resultado = new List<Object[]>();
            Object[] temp = new Object[10];
            List<ListarMatricula_Result> ls = datos.ListarMatricula();

            for (int i = 0; i <= ls.Count() - 2; i++)
            {
                temp = new Object[20];
                temp[0] = ("Consulta exitosa");
                temp[1] = "" + ls[i].Nombre;
                temp[2] = "" + ls[i].PrimerApellido;
                temp[3] = "" + ls[i].SegundoApellido;
                temp[4] = "" + ls[i].Valor;
                temp[5] = "" + ls[i].contacto;
                temp[7] = "" + ls[i].escuela;
                temp[8] = "" + ls[i].gradoid;
                temp[9] = "" + ls[i].estado;
                i++;
                temp[6] = "" + ls[i].contacto;

                resultado.Add(temp);

            }

            return resultado;

        }

        public List<Object[]> BuscarLista(string bus, bool c1, bool c2)
        {
            List<Object[]> resultado = new List<Object[]>();
            Object[] temp = new Object[10];

            if (c1)
            {
                List<ListarNombre_Result> ls = datos.ListarNombre(bus);
                for (int i = 0; i <= ls.Count() - 2; i++)
                {
                    temp = new Object[10];
                    temp[0] = ("Consulta exitosa");
                    temp[1] = "" + ls[i].Nombre;
                    temp[2] = "" + ls[i].PrimerApellido;
                    temp[3] = "" + ls[i].SegundoApellido;
                    temp[4] = "" + ls[i].Valor;
                    temp[5] = "" + ls[i].contacto;
                    temp[7] = "" + ls[i].escuela;
                    temp[8] = "" + ls[i].gradoid;
                    temp[9] = "" + ls[i].estado;
                    i++;
                    temp[6] = "" + ls[i].contacto;

                    resultado.Add(temp);

                }
                return resultado;
            }
            else if (c2)
            {
                List<LlenarDatos_Result> ls = datos.LlenarDatos(bus);
                for (int i = 0; i <= ls.Count() - 2; i++)
                {
                    temp = new Object[10];
                    temp[0] = ("Consulta exitosa");
                    temp[1] = "" + ls[i].Nombre;
                    temp[2] = "" + ls[i].PrimerApellido;
                    temp[3] = "" + ls[i].SegundoApellido;
                    temp[4] = "" + ls[i].Valor;
                    temp[5] = "" + ls[i].contacto;
                    temp[7] = "" + ls[i].escuela;
                    temp[8] = "" + ls[i].gradoid;
                    i++;
                    temp[6] = "" + ls[i].contacto;

                    resultado.Add(temp);

                }
                return resultado;
            }
            else
            {
                bool temp2 = VerificarLong(bus);
                long h = 0;
                if (temp2)
                {
                    h = Int64.Parse(bus);
                }
                List<ListarID_Result> ls = datos.ListarID(h);
                for (int i = 0; i <= ls.Count() - 2; i++)
                {
                    temp = new Object[10];
                    temp[0] = ("Consulta exitosa");
                    temp[1] = "" + ls[i].Nombre;
                    temp[2] = "" + ls[i].PrimerApellido;
                    temp[3] = "" + ls[i].SegundoApellido;
                    temp[4] = "" + ls[i].Valor;
                    temp[5] = "" + ls[i].contacto;
                    temp[7] = "" + ls[i].escuela;
                    temp[8] = "" + ls[i].gradoid;
                    i++;
                    temp[6] = "" + ls[i].contacto;

                    resultado.Add(temp);

                }
                return resultado;
            }


        }

        public void Borrar(string c)
        {
            datos.Borrar(c);
        }

        public string[] LlenarMatricula(string c)
        {
            List<LlenarMatricula_Result> ls = datos.LlenarMatricula(c);


            string[] temp = new string[20];
            temp[1] = ls[0].sN;
            temp[2] = "" + ls[0].sL1;
            temp[3] = "" + ls[0].sL2;
            temp[4] = "" + ls[0].id;
            temp[5] = "" + ls[0].idT;
            temp[6] = "" + ls[0].sNE;
            temp[7] = "" + ls[0].sLNE1;
            temp[8] = "" + ls[0].sLNE2;
            temp[9] = "" + ls[0].idE;
            temp[10] = "" + ls[0].idTE;
            temp[11] = "" + ls[0].procedente;
            temp[12] = "" + ls[0].gen;
            temp[13] = "" + ls[0].genE;
            temp[14] = "" + ls[0].grado;
            temp[15] = "" + ls[0].contacto;
            temp[0] = "" + ls[1].contacto;


            return temp;
        }

        public bool ActualizarMatricula(string correo, string sN, string sL1, string sL2, long id, byte idT, string sNE, string sLE1, string sLE2, long idE, byte idTE, string procedence, int gen, int genE, int grado, int telf, byte[] doc)
        {
            return datos.ActualizarMatricula(correo, sN, sL1, sL2, id, idT, sNE, sLE1, sLE2, idE, idTE, procedence, gen, genE, grado, telf, doc);
        }

        public bool ActualizarEstado(string c, int e)
        {
            return datos.ActualizarEstado(c, e);
        }

        public List<Object[]> ListarUsuarios()
        {
            List<Usuarios> ls = datos.ListarUsuarios();

            List<Object[]> resultado = new List<Object[]>();

            string[] temp = new string[20];

            foreach (Usuarios u in ls)
            {
                temp = new string[20];

                temp[1] = "" + u.nombre;
                temp[2] = "" + u.correo;
                temp[3] = "" + u.telefono;
                temp[4] = "" + u.nivel;
                resultado.Add(temp);
            }



            return resultado;
        }

        public string BorrarUsuario(string c)
        {
            bool temp = datos.BorrarUsuario(c);
            if (temp)
            {
                return "User deleted successfully";
            }
            return "User delete Failed!";
        }

        public string ActualizarPermiso(string c)
        {
            bool temp = datos.ActualizarPermiso(c);
            if (temp)
            {
                return "User permission status changed successfully";
            }
            return "Userpermission status change Failed!";
        }

        public List<Object[]> BuscarUsuarios(string bus, bool c1, bool c2, bool c3, bool c4)
        {
            List<Object[]> resultado = new List<Object[]>();
            Object[] temp = new Object[10];

            byte i = 0;
            if (c1)
            {
                i = 1;
            }
            else if (c2)
            {
                i = 2;
            }
            else if (c3)
            {
                i = 3;
            }
            else if (c4)
            {
                i = 4;
            }

            List<Usuarios> ls = datos.BuscarUsuarios(bus, i);

            foreach (Usuarios u in ls)
            {
                temp = new string[20];

                temp[1] = "" + u.nombre;
                temp[2] = "" + u.correo;
                temp[3] = "" + u.telefono;
                temp[4] = "" + u.nivel;
                resultado.Add(temp);
            }

            return resultado;

        }

        public List<Object[]> BuscarReportes(string bus, bool c1, bool c2, bool c3, bool c4, bool c5)
        {
            List<Object[]> resultado = new List<Object[]>();
            Object[] temp = new Object[10];

            byte a = 0;
            if (c1)
            {
                a = 1;
            }
            else if(c2)
            {
                a = 2;
            }
            else if (c3)
            {
                a = 3;
            }
            else if (c4)
            {
                a = 4;
            }
            else if (c5)
            {
                a = 5;
            }
            List<ListarEstadoNombre_Result> lsn = null;
            List<ListarEstado_Result> ls = null;

            if (bus == null || bus.Equals(""))
            {
                ls = datos.BuscarReportes(a);
            }
            else
            {
                lsn = datos.BuscarReportesNombre(bus, a);
            }

            if(ls != null)
            {
                for (int i = 0; i <= ls.Count() - 2; i++)
                {
                    temp = new Object[20];
                    temp[0] = ("Consulta exitosa");
                    temp[1] = "" + ls[i].Nombre;
                    temp[2] = "" + ls[i].PrimerApellido;
                    temp[3] = "" + ls[i].SegundoApellido;
                    temp[4] = "" + ls[i].Valor;
                    temp[5] = "" + ls[i].contacto;
                    temp[7] = "" + ls[i].escuela;
                    temp[8] = "" + ls[i].gradoid;
                    i++;
                    temp[6] = "" + ls[i].contacto;

                    resultado.Add(temp);

                }
                return resultado;
            }
            else
            {
                for (int i = 0; i <= lsn.Count() - 2; i++)
                {
                    temp = new Object[20];
                    temp[0] = ("Consulta exitosa");
                    temp[1] = "" + lsn[i].Nombre;
                    temp[2] = "" + lsn[i].PrimerApellido;
                    temp[3] = "" + lsn[i].SegundoApellido;
                    temp[4] = "" + lsn[i].Valor;
                    temp[5] = "" + lsn[i].contacto;
                    temp[7] = "" + lsn[i].escuela;
                    temp[8] = "" + lsn[i].gradoid;
                    i++;
                    temp[6] = "" + lsn[i].contacto;

                    resultado.Add(temp);

                }
                return resultado;
            }
                
        }
    }
    
}