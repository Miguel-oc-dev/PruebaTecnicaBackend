using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdministracionEventos.Models;

namespace AdministracionEventos.Controllers
{
    public class AccountController : Controller
    {
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Correo = "admin@ejemplo.com", Contraseña = "12345" }, // Usuario de prueba
            new Usuario { Correo = "user@ejemplo.com", Contraseña = "password" }  // Otro usuario
        };

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuarios.Any(u => u.Correo == usuario.Correo && u.Contraseña == usuario.Contraseña))
                {
                    Session["Usuario"] = usuario.Correo;

                    return RedirectToAction("Index", "Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                }
            }
            return View(usuario);
        }

        // Cerrar sesión
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
