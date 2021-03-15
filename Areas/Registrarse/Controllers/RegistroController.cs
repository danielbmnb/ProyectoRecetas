using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Areas.Ingrediente.Models;
using Recetas.Areas.Recetas.Models;
using Recetas.Areas.Registrarse.Models;
using Recetas.Data;
namespace Recetas.Areas.Registrarse.Controllers
{
    [Area("Registrarse")]

    public class RegistroController : Controller
    {
        public ApplicationDbContext _dbContext;
        public RegistroController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IActionResult Admin()
        {

            return View();
        }

        public IActionResult Registro()
        {

            return View();
        }

        public IActionResult Usuario()
        {
            return View();
        }
        public IActionResult Registrar(string cedula, string nombre, string email, string usuario, string pass)
        {

            Registro registro = new Registro();

            registro.cedula = cedula;
            registro.nombre = nombre;
            registro.email = email;
            registro.usuario = usuario;
            registro.pass = pass;
            _dbContext.Add(registro);
            _dbContext.SaveChanges();

            return View("../../Views/Home/Index");
        }

        public IActionResult Iniciar(Registro registro)
        {

            List<Registro> personas = _dbContext.Registros.ToList();
            Registro busqueda = personas.Find(e => e.usuario == registro.usuario && e.pass == registro.pass);

            if (busqueda == null)

            {
                View("../../Views/Home/Index");
            }
            if (busqueda.rol == "Usuario")
            {
                string id = busqueda.idusuario.ToString();

                HttpContext.Session.SetString("idusuario", id);
                return View("Usuario");
            }
            else if (busqueda.rol == "Admin")
            {

                return View("Admin");

            }

            return RedirectToAction("Inicio");

        }

        public IActionResult enviarEMail(Registro registro)
        {
            string email = registro.email;
            string pass = registro.pass;

            List<Registro> personas = _dbContext.Registros.ToList();
            int respuesta = personas.FindIndex(e => e.email == registro.email || e.pass == registro.pass);

            if (respuesta != -1)
            {
                MailMessage Correo = new MailMessage();
                Correo.From = new MailAddress("contra3021@gmail.com");
                Correo.To.Add(email);
                Correo.Subject = ("Recuperar Contraseña Mis Recetas");
                Correo.Body = "!Hola! Se te ha olvidado la contraseña, tu contraseña es : " + pass;
                Correo.Priority = MailPriority.Normal;

                SmtpClient ServerEmail = new SmtpClient();
                ServerEmail.Credentials = new NetworkCredential("contra3021@gmail.com", "contra12345");
                ServerEmail.Host = "smtp.gmail.com";
                ServerEmail.Port = 587;
                ServerEmail.EnableSsl = true;

                try
                {
                    ServerEmail.Send(Correo);
                }

                catch (Exception e)
                {
                    Console.Write(e);
                }

                Correo.Dispose();

                return View("../../Views/Home/confirmar");

            }
            return View("../../Views/Home/Index");

        }


        public IActionResult CerrarSesion()
        {
            return View("../../Views/Home/Index");
        }

        public IActionResult ListarUsuarios()
        {
            List<Registro> registrarse = _dbContext.Registros.ToList();
            registrarse.ToList();
            return View(registrarse);
        }
        [HttpPost]
        public IActionResult Eliminar(int id)
        {

            Registro registro = _dbContext.Registros.Find(id);

            if (registro == null)
            {
                return View("Admin");
            }
            _dbContext.Registros.Remove(registro);
            _dbContext.SaveChanges();

            return View("ListarUsuarios");
        }


    }

}



