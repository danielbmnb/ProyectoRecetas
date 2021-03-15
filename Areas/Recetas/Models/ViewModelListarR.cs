using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Recetas.Models
{
    public class ViewModelListarR
    {
        public int cantidad { set; get; }
        public int id_ingrediente { set; get; }
        public string nombre_receta { set; get; }
        public int id_receta { set; get; }
        public int id_usuario { set; get; }
        public bool estado { set; get; }

    }
}
