using Microsoft.AspNetCore.Http;
using Recetas.Areas.Recetas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Ingrediente.Models
{
    public class Ingredientes
    {
        [Key]
        public int idingrediente { set; get; }
        public string tipo { set; get; }
        public string descripcion { set; get; }
        public string imagen { set; get; }
        public bool Estado { set; get; } = true;

        [NotMapped]
        public IFormFile Imagenes { set; get; }
        public ICollection<detalle_receta_usuario> detalle { set; get; }


    }
}
