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
    public class ParkingTypesController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();

        // GET: ParkingTypes
        public ActionResult Index()
        {
            return View(db.ParkingTypes.ToList());
        }

        // GET: ParkingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingType parkingType = db.ParkingTypes.Find(id);
            if (parkingType == null)
            {
                return HttpNotFound();
            }
            return View(parkingType);
        }

        // GET: ParkingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkingTypeID,ParkingTypeName,ParkingSpots,ParkingSecurity")] ParkingType parkingType)
        {
            if (ModelState.IsValid)
            {
                db.ParkingTypes.Add(parkingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkingType);
        }

        // GET: ParkingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingType parkingType = db.ParkingTypes.Find(id);
            if (parkingType == null)
            {
                return HttpNotFound();
            }
            return View(parkingType);
        }

        // POST: ParkingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParkingTypeID,ParkingTypeName,ParkingSpots,ParkingSecurity")] ParkingType parkingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingType).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkingType);
        }

        // GET: ParkingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingType parkingType = db.ParkingTypes.Find(id);
            if (parkingType == null)
            {
                return HttpNotFound();
            }
            return View(parkingType);
        }

        // POST: ParkingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingType parkingType = db.ParkingTypes.Find(id);
            db.ParkingTypes.Remove(parkingType);
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
