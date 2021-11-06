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




    }
}