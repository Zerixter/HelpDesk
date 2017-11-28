using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDeskN.Models;

namespace HelpDeskN.Controllers
{
    public class TecnicsController : Controller
    {
        private Context db = new Context();

        // GET: Tecnics
        public ActionResult Index()
        {
            return View(db.Tecnics.ToList());
        }

        // GET: Tecnics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnic tecnic = db.Tecnics.Find(id);
            if (tecnic == null)
            {
                return HttpNotFound();
            }
            return View(tecnic);
        }

        // GET: Tecnics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tecnics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTecnic,NomTecnic,EsActiu")] Tecnic tecnic)
        {
            if (ModelState.IsValid)
            {
                db.Tecnics.Add(tecnic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tecnic);
        }

        // GET: Tecnics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnic tecnic = db.Tecnics.Find(id);
            if (tecnic == null)
            {
                return HttpNotFound();
            }
            return View(tecnic);
        }

        // POST: Tecnics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTecnic,NomTecnic,EsActiu")] Tecnic tecnic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tecnic);
        }

        // GET: Tecnics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnic tecnic = db.Tecnics.Find(id);
            if (tecnic == null)
            {
                return HttpNotFound();
            }
            return View(tecnic);
        }

        // POST: Tecnics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnic tecnic = db.Tecnics.Find(id);
            db.Tecnics.Remove(tecnic);
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
