using Agenda2868022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Agenda2868022.Controllers
{
    public class AprendizesController : Controller
    {
        private Agenda2868022Context db= new Agenda2868022Context();

        [HttpGet]
        public ActionResult Index()
        {
            //Recuperar la relación con ficha
            var aprendiz = db.Aprendizes.Include(a => a.Ficha);            
            return View(db.Aprendizes.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo");
            ViewBag.Genero = ObtenerGenero();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Aprendiz aprendiz) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Aprendizes.Add(aprendiz);
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
                    else if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCelular"))
                    {
                        ViewBag.ErrorCel = "No se pueden registrar celulares repetidos";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }                   
                    ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo",aprendiz.FichaId);
                    ViewBag.Genero = ObtenerGenero();
                    return View(aprendiz);
                }
            }
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            ViewBag.Genero = ObtenerGenero();
            return View(aprendiz);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Aprendiz aprendiz = db.Aprendizes.Find(id);
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo",aprendiz.FichaId);
            ViewBag.Genero = ObtenerGenero();
            return View(aprendiz);
        }
        [HttpPost]
        public ActionResult Edit(Aprendiz aprendiz)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(aprendiz).State = EntityState.Modified;    
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
                    else if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexCelular"))
                    {
                        ViewBag.ErrorCel = "No se pueden registrar celulares repetidos";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                    ViewBag.Genero = ObtenerGenero();
                    return View(aprendiz);
                }
            }
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            ViewBag.Genero = ObtenerGenero();
            return View(aprendiz);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Aprendiz aprendiz= db.Aprendizes.Find(id);
            return View(aprendiz);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Aprendiz aprendiz = db.Aprendizes.Find(id);
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            return View(aprendiz);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Aprendiz aprendiz = db.Aprendizes.Find(id);
            try
            {
                db.Aprendizes.Remove(aprendiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(aprendiz);
            }

        }

        private List<SelectListItem> ObtenerGenero()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="Seleccionar Genero",
                    Value="Seleccionar",
                    Selected=true
                },
                new SelectListItem()
                {
                    Text="Femenino",
                    Value="Femenino"                    
                },
                new SelectListItem()
                {
                    Text="Masculino",
                    Value="Masculino"
                },
                new SelectListItem()
                {
                    Text="Otros",
                    Value="Otros"
                },
            };
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