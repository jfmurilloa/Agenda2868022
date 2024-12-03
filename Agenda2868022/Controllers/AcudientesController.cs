using Agenda2868022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda2868022.Controllers
{
    public class AcudientesController : Controller
    {
        private Agenda2868022Context db = new Agenda2868022Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Acudientes.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Acudiente acudiente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Acudientes.Add(acudiente);
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
                    return View(acudiente);
                }
            }
            return View();
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