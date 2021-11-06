using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrolmentSystem_BEHS.Datos
{
    public class CapaDatos
    {

        private EnrollEntities entity = new EnrollEntities();

        public CapaDatos()
        {

        }

        public int TitleDate()
        {
            using (entity = new EnrollEntities())
            {
                int anno = entity.FechaAnno();
                return anno;
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

        public void Registrar(string c, string p)
        {
            using (entity = new EnrollEntities())
            {
                entity.CrearUsuario(c,p);
                
            }
        }


    }
}