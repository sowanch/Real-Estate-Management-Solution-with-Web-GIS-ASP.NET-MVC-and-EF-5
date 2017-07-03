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
    public class ApartmentStatusTrailsController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();

        // GET: ApartmentStatusTrails
        public ActionResult Index()
        {
            var apartmentStatusTrails = db.ApartmentStatusTrails.Include(a => a.Agent).Include(a => a.ApartmentRentee).Include(a => a.Apartment);
            return View(apartmentStatusTrails.ToList());
        }

        // GET: ApartmentStatusTrails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentStatusTrail apartmentStatusTrail = db.ApartmentStatusTrails.Find(id);
            if (apartmentStatusTrail == null)
            {
                return HttpNotFound();
            }
            return View(apartmentStatusTrail);
        }

        // GET: ApartmentStatusTrails/Create
        public ActionResult Create()
        {
            ViewBag.AgentInvolved = new SelectList(db.Agents, "AgentID", "AgentName");
            ViewBag.ApartmentRenteeID = new SelectList(db.ApartmentRentees, "ApartmentRenteeID", "ApartmentRenteeName");
            ViewBag.ApartmentID = new SelectList(db.Apartments.Where(v => v.ApartmentStatus.Equals(true)), "ApartmentID", "ApartmentName");
            return View();
        }

        // POST: ApartmentStatusTrails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentStatusTrailID,ApartmentID,ApartmentRenteeID,AgentInvolved,StartDate,EndDate")] ApartmentStatusTrail apartmentStatusTrail)
        {
            if (ModelState.IsValid)
            {
                //Checkout the apartment from the availability list
                Apartment changedApt = db.Apartments.Where(v => v.ApartmentID.Equals(apartmentStatusTrail.ApartmentID)).Single();
                changedApt.ApartmentStatus = false;

                db.ApartmentStatusTrails.Add(apartmentStatusTrail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentInvolved = new SelectList(db.Agents, "AgentID", "AgentName", apartmentStatusTrail.AgentInvolved);
            ViewBag.ApartmentRenteeID = new SelectList(db.ApartmentRentees, "ApartmentRenteeID", "ApartmentRenteeName", apartmentStatusTrail.ApartmentRenteeID);
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentName", apartmentStatusTrail.ApartmentID);
            return View(apartmentStatusTrail);
        }

        // GET: ApartmentStatusTrails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentStatusTrail apartmentStatusTrail = db.ApartmentStatusTrails.Find(id);
            if (apartmentStatusTrail == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentInvolved = new SelectList(db.Agents, "AgentID", "AgentName", apartmentStatusTrail.AgentInvolved);
            ViewBag.ApartmentRenteeID = new SelectList(db.ApartmentRentees, "ApartmentRenteeID", "ApartmentRenteeName", apartmentStatusTrail.ApartmentRenteeID);
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentName", apartmentStatusTrail.ApartmentID);
            return View(apartmentStatusTrail);
        }

        // POST: ApartmentStatusTrails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentStatusTrailID,ApartmentID,ApartmentRenteeID,AgentInvolved,StartDate,EndDate")] ApartmentStatusTrail apartmentStatusTrail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentStatusTrail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentInvolved = new SelectList(db.Agents, "AgentID", "AgentName", apartmentStatusTrail.AgentInvolved);
            ViewBag.ApartmentRenteeID = new SelectList(db.ApartmentRentees, "ApartmentRenteeID", "ApartmentRenteeName", apartmentStatusTrail.ApartmentRenteeID);
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentName", apartmentStatusTrail.ApartmentID);
            return View(apartmentStatusTrail);
        }

        // GET: ApartmentStatusTrails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentStatusTrail apartmentStatusTrail = db.ApartmentStatusTrails.Find(id);
            if (apartmentStatusTrail == null)
            {
                return HttpNotFound();
            }
            return View(apartmentStatusTrail);
        }

        // POST: ApartmentStatusTrails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApartmentStatusTrail apartmentStatusTrail = db.ApartmentStatusTrails.Find(id);
            db.ApartmentStatusTrails.Remove(apartmentStatusTrail);
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
