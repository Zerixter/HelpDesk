using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDeskN.Models;
using System.Data.Entity.Validation;

namespace HelpDeskN.Controllers
{
    public class IncidenciasController : Controller
    {
        private Context db = new Context();

        private IQueryable<Tecnic> TecnicsActius {
            get { return db.Tecnics.Where(x => x.EsActiu); }
        }

        // GET: Incidencias
        public ActionResult Index()
        {
            var incidencies = db.Incidencies.Include(i => i.TecnicQueObreLaIncidencia).Include(i => i.TecnicQueTancaLaIncidencia);
            return View(incidencies.ToList());
        }

        // GET: Incidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencies.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // GET: Incidencias/Create
        public ActionResult Create()
        {
            ViewBag.IdTecnicQueObreLaIncidencia = new SelectList(TecnicsActius, "IdTecnic", "NomTecnic");
            //ViewBag.IdTecnicQueTancaLaIncidencia = new SelectList(db.Tecnics, "IdTecnic", "NomTecnic");
            return View();
        }

        // POST: Incidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIncidencia,DataAltaIncidencia,DescripcioCurta,IdTecnicQueObreLaIncidencia,IdTecnicQueTancaLaIncidencia")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Incidencies.Add(incidencia);
                try
                {
                    db.SaveChanges();
                } catch (DbEntityValidationException ex)
                {
                    var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                    this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdTecnicQueObreLaIncidencia = new SelectList(TecnicsActius, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueObreLaIncidencia);
            //ViewBag.IdTecnicQueTancaLaIncidencia = new SelectList(db.Tecnics, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueTancaLaIncidencia);
            return View(incidencia);
        }

        // GET: Incidencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencies.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IdTecnicQueObreLaIncidencia = new SelectList(db.Tecnics, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueObreLaIncidencia);
            ViewBag.IdTecnicQueTancaLaIncidencia = new SelectList(TecnicsActius, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueTancaLaIncidencia);
            return View(incidencia);
        }

        // POST: Incidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIncidencia,DataAltaIncidencia,DescripcioCurta,IdTecnicQueObreLaIncidencia,IdTecnicQueTancaLaIncidencia")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidencia).State = EntityState.Modified;
                incidencia.IdTecnicQueObreLaIncidencia = db.
                    Incidencies.
                    Where(x => x.IdIncidencia == incidencia.IdIncidencia).
                    Select(x => x.IdTecnicQueObreLaIncidencia).
                    FirstOrDefault();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IdTecnicQueObreLaIncidencia = new SelectList(db.Tecnics, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueObreLaIncidencia);
            ViewBag.IdTecnicQueTancaLaIncidencia = new SelectList(TecnicsActius, "IdTecnic", "NomTecnic", incidencia.IdTecnicQueTancaLaIncidencia);
            return View(incidencia);
        }

        // GET: Incidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencies.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // POST: Incidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidencia incidencia = db.Incidencies.Find(id);
            db.Incidencies.Remove(incidencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
