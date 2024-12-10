using Agenda2868022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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

            var view = new FichaDetailsView
            {
                FichaId= ficha.FichaId,
                Codigo= ficha.Codigo,
                Programa= ficha.Programa,
                FechaInicio= ficha.FechaInicio,
                FechaFin= ficha.FechaFin,
                FichaInstructors = ficha.FichaInstructors.ToList(),
            };

            return View(view);
            
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

        [HttpGet]
        public ActionResult AddInstructor(int fichaId)
        {
            ViewBag.InstructorId = new SelectList(db.Instructors.OrderBy(
                i => i.Nombres), "InstructorId", "FullName");

            var FichaInst = new FichaInstructorView
            {
                FichaId= fichaId,
            };
            return View(FichaInst);
        }

        [HttpPost]
        public ActionResult AddInstructor(FichaInstructorView fichaInst)
        {
            ViewBag.InstructorId = new SelectList(db.Instructors.OrderBy(
                i => i.Nombres), "InstructorId", "FullName");

            //verificar si el instructor esta en la ficha
            var instFicha= db.FichaInstructors.Where(fi => fi.FichaId== fichaInst.FichaId
              && fi.InstructorId== fichaInst.InstructorId).FirstOrDefault();

            if (instFicha != null)
            {
                ViewBag.Error = "El instructor ya está vinculado a la ficha";
            }
            else
            {
                instFicha = new FichaInstructor
                {
                    FichaId=fichaInst.FichaId,
                    InstructorId=fichaInst.InstructorId
                };
                db.FichaInstructors.Add(instFicha);
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", fichaInst.FichaId));                    
            }
            return View(fichaInst);

        }

        public ActionResult DeleteInstructor(int id)
        {
            var instructor = db.FichaInstructors.Find(id);
            if (instructor != null) 
            {
                db.FichaInstructors.Remove(instructor);
                db.SaveChanges();
            }
            return RedirectToAction(string.Format("Details/{0}", instructor.FichaId));
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