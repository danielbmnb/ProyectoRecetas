using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recetas.Areas.Registrarse.Models;
using Recetas.Areas.Recetas.Models;
using System.Threading.Tasks;
using Recetas.Areas.Ingrediente.Models;

namespace Recetas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Registro> Registros { set; get; }
        public DbSet<Receta> recetas { set; get; }
        public DbSet<Ingredientes> ingredientes { set; get; }
        public DbSet<detalle_receta_usuario> detalle_receta_usuario { get; set; }
    }
}

