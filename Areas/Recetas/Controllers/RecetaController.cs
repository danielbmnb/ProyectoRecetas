using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Recetas.Areas.Ingrediente.Models;
using Recetas.Areas.Recetas.Models;
using Recetas.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Recetas.Areas.Registrarse.Models;
using Microsoft.AspNetCore.Http;

namespace Recetas.Areas.Recetas.Controllers
{
    [Area("Recetas")]
    public class RecetaController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public RecetaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RecetasUsuarios() {

            Ingredientes ingredientes = new Ingredientes();
            string tipo1 = ingredientes.tipo;

            var detalleR = _dbContext.Registros
                .Join(
                _dbContext.recetas,
                 usuario => usuario.idusuario,
                 recetas => recetas.idusuario,
                 (usuario, recetas) => new detalle_receta
                 {
                     id_receta = recetas.IdReceta,
                     id_usuario = usuario.idusuario,
                     estado = recetas.Estado,
                     nombre = recetas.nombre,
                     nombre_usuario = usuario.nombre,
                     tipo = tipo1,
                     fecha = recetas.Fecha,
                 
                 }
                ).Where(e=>e.estado == true).ToList();
            List<Ingredientes> r = _dbContext.ingredientes.ToList();
            ViewData["lista"] = r;
            return View(detalleR);

        }
        public IActionResult ListarReceta()
        {
            string valor = HttpContext.Session.GetString("idusuario");
            int usuario = int.Parse(valor);

            var material = _dbContext.recetas
                .Join(
                  _dbContext.detalle_receta_usuario,
                  receta => receta.IdReceta,
                  detalle => detalle.IdReceta,
                  (receta, detalle) => new ViewModelListarR
                  {
                      id_ingrediente = detalle.idingrediente,
                      cantidad = detalle.cantidad,
                      id_receta = receta.IdReceta,
                      estado = receta.Estado,
                      id_usuario = receta.idusuario,
                      nombre_receta = receta.nombre
                  }
            ).Where(e => e.id_usuario == usuario).ToList();

            int c;
            foreach (ViewModelListarR e in material) {
                 c= e.id_ingrediente;

            }

            List<Ingredientes>r = _dbContext.ingredientes.ToList();
            ViewData["lista"] = r;
            List<Receta> r2 = _dbContext.recetas.ToList();
            ViewData["lista2"] = r2;
            return View(material);
        }

        public IActionResult Agregar()
        {
            Receta receta = new Receta();
            receta.ingrediente = _dbContext.ingredientes.ToList();

            return View(receta);
        }

        public IActionResult Estado(int id)
        {
            Receta recetas = _dbContext.recetas.Find(id);
            if (recetas == null)
            {
                return RedirectToAction("ListarReceta");

            }
            recetas.Estado = !recetas.Estado;

            _dbContext.Update(recetas);
            _dbContext.SaveChanges();

            return RedirectToAction("ListarReceta");

        }

        public IActionResult Detalle(RecetaUsuario recetaUsuario)
        {
            string valor = HttpContext.Session.GetString("idusuario");
            int usuario = Int32.Parse(valor);

            Receta receta = new Receta { nombre = recetaUsuario.RecetaNombre, idusuario = usuario };
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Add(receta);
                    _dbContext.SaveChanges();
                    int cantidad = recetaUsuario.Cantidad;

                    foreach (Ingredientes item in recetaUsuario.Receta)
                    {
                        detalle_receta_usuario detalle = new detalle_receta_usuario
                        {
                            idingrediente = recetaUsuario.IngredienteId,
                            IdReceta = receta.IdReceta,
                            cantidad = cantidad
                        };

                        _dbContext.Add(detalle);
                    }
                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
            return RedirectToAction("ListarReceta");
        }

    }
}
