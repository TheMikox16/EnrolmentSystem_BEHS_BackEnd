using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EnrolmentSystem_BEHS.Datos
{
    public class CapaDatos
    {
        private SqlConnection connextion;
        private string estado = "desconectado";
        private EnrollEntities entity = new EnrollEntities();

        public CapaDatos()
        {
            enlazar();
        }

        public int TitleDate()
        {
            using (entity = new EnrollEntities())
            {
                int anno = entity.FechaAnno();
                return anno;
            }
        }

        public void enlazar()
        {
            try
            {
                //Se establece la conexion seguna la variable connectionString del archivo config del programa
                connextion = new SqlConnection();
                connextion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                connextion.Open(); //establece conexion

                /**linq.Connection.ConnectionString = connextion.ConnectionString;
                linq.Connection.Open();*/

                estado = "conectado"; //conexion exitosa
            }
            catch (Exception ex)
            {
                estado = "desconectado"; //conexion fallida despues de usar conexion.open()
            }


        }

        public List<Usuarios> Ingresar(string c, string p)
        {
            using (entity = new EnrollEntities())
            {
                List<Usuarios> busqueda = entity.Usuarios.Where(a => a.correo.Equals(c) && a.contrasena.Equals(p)).ToList();

                return busqueda;
            }
        }

        public bool Registrar(string c, string p)
        {
            using (entity = new EnrollEntities())
            {

                List<Usuarios> busqueda = entity.Usuarios.Where(a => a.correo.Equals(c)).ToList();

                if(busqueda.Count() > 0)
                {
                    return false;
                }

                entity.CrearUsuario(c,p);
                return true;
                
            }
        }


        public bool Matricular(string correo, string sN, string sL1, string sL2, long id, byte idT, string sNE, string sLE1, string sLE2, long idE, byte idTE, string procedence, int gen, int genE, int grado, int telf)
        {
            using (entity = new EnrollEntities())
            {
                entity.Matricular(correo, sN, sL1, sL2, id, idT, sNE, sLE1, sLE2, idE, idTE, procedence, gen, genE, grado, telf);

                return true;
            }
        }


    }
}