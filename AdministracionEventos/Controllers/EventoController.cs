using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdministracionEventos.Models;

namespace AdministracionEventos.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Evento.ToList());
            }
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                Evento evento = context.Evento.FirstOrDefault(x => x.ID == id);

                if (evento == null)
                {
                    return HttpNotFound();  
                }

                return View(evento);  
            }
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        public ActionResult Create(Evento evento)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Evento.Add(evento);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                // Obtener el evento con el id proporcionado
                var evento = context.Evento.FirstOrDefault(x => x.ID == id);

                if (evento == null)
                {
                    return HttpNotFound(); // Si no se encuentra el evento
                }

                // Cargar la lista de usuarios para el DropDownList antes de cerrar el DbContext
                var usuarios = context.Usuario.ToList(); // Obtener todos los usuarios
                ViewBag.Usuarios = new SelectList(usuarios, "ID", "Nombre", evento.UsuarioID); // Pasar a ViewBag

                // Regresar el modelo con la lista de usuarios cargada
                return View(evento);
            }
        }



        // POST: Evento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Evento evento)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(evento).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Evento.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Evento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                { 
                    Evento evento = context.Evento.Where(x=>x.ID == id).FirstOrDefault();
                    context.Evento.Remove(evento);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
