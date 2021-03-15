using Recetas.Areas.Recetas.Models;
using Recetas.Areas.Registrarse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recetas.Areas.Registrarse.Models
{
    public class Registro
    {
        [Key]
        public int idusuario { set; get; }
        public string cedula { set; get; }
        public string nombre { set; get; }
        [Required]
        [EmailAddress]
        public string email { set; get; }
        public string usuario { set; get; }
        public string pass { set; get; }
        public string rol { set; get; } = "Usuario";
        [NotMapped]
        public ICollection<Receta> receta { set; get; }

    }
}
