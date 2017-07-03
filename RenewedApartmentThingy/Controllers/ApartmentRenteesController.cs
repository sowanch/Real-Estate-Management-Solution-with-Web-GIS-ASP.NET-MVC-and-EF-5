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
    public class ApartmentRenteesController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();

        // GET: ApartmentRentees
        public ActionResult Index()
        {
            return View(db.ApartmentRentees.ToList());
        }

        // GET: ApartmentRentees/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentRentee apartmentRentee = db.ApartmentRentees.Find(id);
            if (apartmentRentee == null)
            {
                return HttpNotFound();
            }
            return View(apartmentRentee);
        }

        // GET: ApartmentRentees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApartmentRentees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentRenteeID,ApartmentRenteeName,ApartmentRenteeNumber,ApartmentRenterIDNumber,DateOfRegistration")] ApartmentRentee apartmentRentee)
        {
            if (ModelState.IsValid)
            {
                db.ApartmentRentees.Add(apartmentRentee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartmentRentee);
        }

        // GET: ApartmentRentees/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentRentee apartmentRentee = db.ApartmentRentees.Find(id);
            if (apartmentRentee == null)
            {
                return HttpNotFound();
            }
            return View(apartmentRentee);
        }

        // POST: ApartmentRentees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentRenteeID,ApartmentRenteeName,ApartmentRenteeNumber,ApartmentRenterIDNumber,DateOfRegistration")] ApartmentRentee apartmentRentee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentRentee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apartmentRentee);
        }

        // GET: ApartmentRentees/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentRentee apartmentRentee = db.ApartmentRentees.Find(id);
            if (apartmentRentee == null)
            {
                return HttpNotFound();
            }
            return View(apartmentRentee);
        }

        // POST: ApartmentRentees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ApartmentRentee apartmentRentee = db.ApartmentRentees.Find(id);
            db.ApartmentRentees.Remove(apartmentRentee);
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
