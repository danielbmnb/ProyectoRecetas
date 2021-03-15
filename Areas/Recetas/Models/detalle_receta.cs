using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Recetas.Models
{
    public class detalle_receta
    {
        public int id_receta { get; set; }
        public bool estado { get; set; }
        public string nombre { get; set; }
        public int id_usuario { get; set; }
        public string tipo { set; get; }
        public DateTime fecha { set; get; }
       public string nombre_usuario { set; get; }
    }
}
