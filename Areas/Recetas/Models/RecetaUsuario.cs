using Recetas.Areas.Ingrediente.Models;
using Recetas.Areas.Registrarse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Recetas.Models
{
    public class RecetaUsuario
    {
        public int Cantidad { set; get; }
        public int IngredienteId { set; get; }
        public string RecetaNombre { set; get; }
        public int IdUsuario { set; get; }
        public List<Ingredientes> Receta { set; get; }
    }
}
