using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recetas.Areas.Ingrediente.Models;
using Recetas.Data;

namespace Recetas.Areas.Ingrediente.Controllers
{
    [Area("Ingrediente")]
    public class IngredienteController : Controller
    {

        public ApplicationDbContext _dbContext;
        public IngredienteController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agregar()
        {
            Ingredientes ingredientes = new Ingredientes();

            return View(ingredientes);
        }
        public IActionResult GuardarIngrediente(Ingredientes ingrediente)
        {
            if (ingrediente.Imagenes != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes", ingrediente.Imagenes.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    ingrediente.Imagenes.CopyTo(stream);
                    ingrediente.imagen = ingrediente.Imagenes.FileName;
                }
            }
            else
            {
                ingrediente.imagen = "encontrar.png";
            }

            if (ingrediente.idingrediente == 0)
            {
                _dbContext.Add(ingrediente);

            }
            else
            {

                _dbContext.Update(ingrediente);

            }
            _dbContext.SaveChanges();
            return RedirectToAction("ListarIngrediente");
        }
        public IActionResult EstadoIngrediente(int id)
        {
            Ingredientes ingrediente = _dbContext.ingredientes.Find(id);
            if (ingrediente == null)
            {
                return RedirectToAction("ListarIngrediente");

            }
            ingrediente.Estado = !ingrediente.Estado;

            _dbContext.Update(ingrediente);
            _dbContext.SaveChanges();

            return RedirectToAction("ListarIngrediente");

        }

        public IActionResult ModificarIngrediente(int id)
        {
            Ingredientes ingrediente = _dbContext.ingredientes.Find(id);
            if (ingrediente == null)
            {
                return RedirectToAction("ModificarIngrediente");

            }
            return View(ingrediente);
        }

        public IActionResult ListarIngrediente()
        {
            List<Ingredientes> ingredientes = _dbContext.ingredientes.ToList();
            ingredientes.ToList();
            return View(ingredientes);
        }

    }
}