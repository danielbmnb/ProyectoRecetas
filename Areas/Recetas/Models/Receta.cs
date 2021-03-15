using Microsoft.AspNetCore.Http;
using Recetas.Areas.Ingrediente.Models;
using Recetas.Areas.Registrarse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Recetas.Models
{
    public class Receta
    {
        [Key]
        public int IdReceta { set; get; }
        public string nombre { get; set; }
        //public int cantidad { set; get; } = 0;
        public bool Estado { set; get; } = true;
        public DateTime Fecha { set; get; } = DateTime.Now;
        public int idusuario { set; get; }
        public Registro registro { get; set; }
        [NotMapped]
        public detalle_receta_usuario detalleR { set; get; }
        [NotMapped]
        public List<Ingredientes> ingrediente { set; get; }
        
        public ICollection<detalle_receta_usuario> detalle { set; get; }

    }
}
