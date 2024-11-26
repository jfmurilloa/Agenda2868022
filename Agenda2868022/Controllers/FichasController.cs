using Agenda2868022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda2868022.Controllers
{
    public class FichasController : Controller
    {
        //Objeto que representa el contexto
        Agenda2868022Context db = new Agenda2868022Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Fichas.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                if (ficha.FechaFin <= ficha.FechaInicio)
                {
                    ViewBag.ErrorFecha = "La fecha de inicial no puede ser mayor a la final ";
                    return View(ficha);
                }

                try
                {
                    db.Fichas.Add(ficha);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                    {
                        ViewBag.ErrorCodigo = "No es posible registrar fichas con codigo duplicado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(ficha);
                }

            }
            return View(ficha);

            
        }

        [HttpGet]
        public ActionResult Edit(int? id) 
        {
            Ficha ficha = db.Fichas.Find(id);
            return View(ficha);

        }
        [HttpPost]
        public ActionResult Edit(Ficha ficha)
        {
            if (ModelState.IsValid) 
            {
                if (ficha.FechaFin <= ficha.FechaInicio)
                {
                    ViewBag.ErrorFecha = "La fecha de inicial no puede ser mayor a la final ";
                    return View(ficha);
                }
                try
                {
                    db.Entry(ficha).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                    {
                        ViewBag.ErrorCodigo = "No es posible registrar fichas con codigo duplicado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }                    
                    return View(ficha);
                }
            }
            return View(ficha);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Ficha ficha= db.Fichas.Find(id);
            return View(ficha);
            
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Ficha ficha = db.Fichas.Find(id);
            return View(ficha);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Ficha ficha = db.Fichas.Find(id);
            try
            {
                db.Fichas.Remove(ficha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error= ex.Message;
                return View(ficha);
            }
            
        }

        //liberar la conexión con la base de datos
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}