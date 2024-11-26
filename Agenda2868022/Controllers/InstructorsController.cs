using Agenda2868022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda2868022.Controllers
{
    public class InstructorsController : Controller
    {
        //Objeto que representa el contexto
        Agenda2868022Context db = new Agenda2868022Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Instructors.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Instructors.Add(instructor);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.ErrorDoc = "No se pueden registrar documentos repetidos";
                    }
                    else if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCorreo"))
                    {
                        ViewBag.ErrorCor = "No se pueden registrar correos repetidos";
                    }
                    else 
                    {
                        ViewBag.Error = ex.Message;
                    }
                    
                    return View(instructor);
                }

            }
            return View(instructor);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Instructor instructor = db.Instructors.Find(id);
            return View(instructor);

        }
        [HttpPost]
        public ActionResult Edit(Instructor instructor)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(instructor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.ErrorDoc = "No se pueden registrar documentos repetidos";
                    }
                    else if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCorreo"))
                    {
                        ViewBag.ErrorCor = "No se pueden registrar correos repetidos";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }

                    return View(instructor);
                }
            }
            return View(instructor);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Instructor instructor = db.Instructors.Find(id);
            return View(instructor);

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Instructor instructor = db.Instructors.Find(id);
            return View(instructor);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            try
            {
                db.Instructors.Remove(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(instructor);
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