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
    public class ApartmentOwnersController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();

        // GET: ApartmentOwners
        public ActionResult Index()
        {
            return View(db.ApartmentOwners.ToList());
        }

        // GET: ApartmentOwners/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentOwner apartmentOwner = db.ApartmentOwners.Find(id);
            if (apartmentOwner == null)
            {
                return HttpNotFound();
            }
            return View(apartmentOwner);
        }

        // GET: ApartmentOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApartmentOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentOwnerID,ApartmentOwnerName,ApartmentOwnerNumber,OwnerStartDate,OwnerEndDate")] ApartmentOwner apartmentOwner)
        {
            if (ModelState.IsValid)
            {
                db.ApartmentOwners.Add(apartmentOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartmentOwner);
        }

        // GET: ApartmentOwners/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentOwner apartmentOwner = db.ApartmentOwners.Find(id);
            if (apartmentOwner == null)
            {
                return HttpNotFound();
            }
            return View(apartmentOwner);
        }

        // POST: ApartmentOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentOwnerID,ApartmentOwnerName,ApartmentOwnerNumber,OwnerStartDate,OwnerEndDate")] ApartmentOwner apartmentOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentOwner).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apartmentOwner);
        }

        // GET: ApartmentOwners/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentOwner apartmentOwner = db.ApartmentOwners.Find(id);
            if (apartmentOwner == null)
            {
                return HttpNotFound();
            }
            return View(apartmentOwner);
        }

        // POST: ApartmentOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ApartmentOwner apartmentOwner = db.ApartmentOwners.Find(id);
            db.ApartmentOwners.Remove(apartmentOwner);
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
