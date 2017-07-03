using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RenewedApartmentThingy;

namespace RenewedApartmentThingy.Controllers
{
    public class ApartmentTypesController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();

        // GET: ApartmentTypes
        public ActionResult Index()
        {
            return View(db.ApartmentTypes.ToList());
        }

        // GET: ApartmentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentType apartmentType = db.ApartmentTypes.Find(id);
            if (apartmentType == null)
            {
                return HttpNotFound();
            }
            return View(apartmentType);
        }

        // GET: ApartmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApartmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentTypeID,ApartmentTypeName,ApartmentTypeDescription")] ApartmentType apartmentType)
        {
            if (ModelState.IsValid)
            {
                db.ApartmentTypes.Add(apartmentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartmentType);
        }

        // GET: ApartmentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentType apartmentType = db.ApartmentTypes.Find(id);
            if (apartmentType == null)
            {
                return HttpNotFound();
            }
            return View(apartmentType);
        }

        // POST: ApartmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentTypeID,ApartmentTypeName,ApartmentTypeDescription")] ApartmentType apartmentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentType).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apartmentType);
        }

        // GET: ApartmentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentType apartmentType = db.ApartmentTypes.Find(id);
            if (apartmentType == null)
            {
                return HttpNotFound();
            }
            return View(apartmentType);
        }

        // POST: ApartmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApartmentType apartmentType = db.ApartmentTypes.Find(id);
            db.ApartmentTypes.Remove(apartmentType);
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
