using Recetas.Areas.Ingrediente.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Recetas.Models
{
    public class detalle_receta_usuario
    {
        [Key]
        public int id_detalle_receta_usuario { set; get; }
        public int IdReceta { set; get;}
        public int idingrediente { set; get; }
        public int cantidad { set; get; } =0;
        //public DateTime Fecha { set; get; } = DateTime.Now;
        public Receta receta { get; set; }
        public Ingredientes ingrediente { get; set; }

    }
}
