using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RenewedApartmentThingy;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RenewedApartmentThingy.Controllers
{
    public class ApartmentsController : Controller
    {
        private RentManagementEntities db = new RentManagementEntities();
        Result response = new Result();
        // GET: Apartments
        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.ApartmentOwner).Include(a => a.ApartmentType1).Include(a => a.ParkingType1);
            return View(apartments.ToList());
        }

        // GET: Apartments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // GET: Apartments/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentOwnerID = new SelectList(db.ApartmentOwners, "ApartmentOwnerID", "ApartmentOwnerName");
            ViewBag.ApartmentType = new SelectList(db.ApartmentTypes, "ApartmentTypeID", "ApartmentTypeName");
            ViewBag.ParkingType = new SelectList(db.ParkingTypes, "ParkingTypeID", "ParkingTypeName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentID,ApartmentOwnerID,ApartmentBuildDate,ApartmentStatus,NumberOfBedrooms,NumberOfBathrooms,ApartmentName,ParkingType,LocationName,CurrentPrice,ApartmentType,ApartmentSize,WaterAvailability,ElectricityAvailability,Latitude,Longitude,Floor,BuildingFloors,BuildingComplexName")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                //Add to GIS Database
                if (!GetWithParams("karanjamwalimu", "Arc9oln!", apartment).resultCode.Equals("Result Code: 0000"))
                {

                    //Add to database
                    db.Apartments.Add(apartment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            ViewBag.ApartmentOwnerID = new SelectList(db.ApartmentOwners, "ApartmentOwnerID", "ApartmentOwnerName", apartment.ApartmentOwnerID);
            ViewBag.ApartmentType = new SelectList(db.ApartmentTypes, "ApartmentTypeID", "ApartmentTypeName", apartment.ApartmentType);
            ViewBag.ParkingType = new SelectList(db.ParkingTypes, "ParkingTypeID", "ParkingTypeName", apartment.ParkingType);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentOwnerID = new SelectList(db.ApartmentOwners, "ApartmentOwnerID", "ApartmentOwnerName", apartment.ApartmentOwnerID);
            ViewBag.ApartmentType = new SelectList(db.ApartmentTypes, "ApartmentTypeID", "ApartmentTypeName", apartment.ApartmentType);
            ViewBag.ParkingType = new SelectList(db.ParkingTypes, "ParkingTypeID", "ParkingTypeName", apartment.ParkingType);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentID,ApartmentOwnerID,ApartmentBuildDate,ApartmentStatus,NumberOfBedrooms,NumberOfBathrooms,ApartmentName,ParkingType,LocationName,CurrentPrice,ApartmentType,ApartmentSize,WaterAvailability,ElectricityAvailability,Latitude,Longitude,Floor,BuildingFloors,BuildingComplexName")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentOwnerID = new SelectList(db.ApartmentOwners, "ApartmentOwnerID", "ApartmentOwnerName", apartment.ApartmentOwnerID);
            ViewBag.ApartmentType = new SelectList(db.ApartmentTypes, "ApartmentTypeID", "ApartmentTypeName", apartment.ApartmentType);
            ViewBag.ParkingType = new SelectList(db.ParkingTypes, "ParkingTypeID", "ParkingTypeName", apartment.ParkingType);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Apartment apartment = db.Apartments.Find(id);
            db.Apartments.Remove(apartment);
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

        public Result GetWithParams(string username, string password, Apartment apart)
        {

            try
            {
                //Get the Portal in question
                AGOL currentPortal = new AGOL(username, password);

                //define the feature service in question
                var featureServiceURL = "http://services.arcgis.com/p2yM6iU800nr9nEC/arcgis/rest/services/Apartments/FeatureServer";

                //define the URL for querying the feature service based on the plot number
                var queryURL = featureServiceURL + "/0/query";

                //define the URL for updating a feature in the Feature Service
                var updateURL = featureServiceURL + "/0/addFeatures";


                var tokenz = currentPortal.Token;

                //Create Features Name Value Collection


                var updateData = new NameValueCollection();
                updateData["token"] = tokenz;
                updateData["f"] = "json";

                long currentID = db.Apartments.Max(v => v.ApartmentID) + 1;

                string jsonf = "[{'geometry':{" + "'x':" + apart.Longitude.ToString() + "," + "'y':" + apart.Latitude.ToString() + "}," +
                "'attributes': {'ApartmentID': " + currentID.ToString() +
                ",'ApartmentOwnerID': " + apart.ApartmentOwnerID.ToString() +
                ",'ApartmentBuildDate': '" + apart.ApartmentBuildDate.ToShortDateString() +
                "', 'ApartmentStatus': '" + apart.ApartmentStatus.ToString() +
                "', 'NumberOfBedrooms': " + apart.NumberOfBedrooms.ToString() +
                ", 'NumberOfBathrooms': " + apart.NumberOfBathrooms.ToString() +
                ", 'ApartmentName': '" + apart.ApartmentName +
                "', 'ParkingType': " + apart.ParkingType.ToString() +
                ", 'LocationName': '" + apart.LocationName +
                "', 'CurrentPrice': " + apart.CurrentPrice.ToString() +
                ", 'ApartmentType': '" + apart.ApartmentType.ToString() +
                "', 'ApartmentSize': " + apart.ApartmentSize.ToString() +
                ", 'WaterAvailability': '" + apart.WaterAvailability.ToString() +
                "', 'ElectricityAvailability': '" + apart.ElectricityAvailability.ToString() +
                "', 'Floor': " + apart.Floor.ToString() +
                ", 'BuildingFloors': " + apart.BuildingFloors.ToString() +
                ", 'BuildingComplexName': '" + apart.BuildingComplexName +
                "','FromDatabase': " + "'Y'" + "}," +
                "'spatialReference' : {" +
                    "'wkid' : '4326'" +
                "}}]";


                updateData["features"] = jsonf;



                string updateResult = @getQueryResponse(updateData, updateURL);

                //Report back on the feature - whether updated or not
                var uObj = JsonConvert.DeserializeObject(updateResult) as JObject;

                if ((JArray)uObj["updateResults"] != null)
                {
                    JArray ufeatures = (JArray)uObj["updateResults"];

                    string boolResult = ((JObject)ufeatures[0])["success"].ToString();

                    response.resultText = updateResult;

                    if (boolResult == "True")
                    {
                        response.resultCode = "Result Code: 0000";
                        response.resultText = "OK! Feature number " + ((JObject)ufeatures[0])["objectId"].ToString() + " successfully updated";
                    }
                    else
                    {
                        response.resultCode = "Result Code: 0001";
                        response.resultText = updateResult + ". Contact Administrator";
                    }

                }
                else
                {
                    response.resultCode = "Result Code: 0003";
                    response.resultText = updateResult + ". Contact Administrator";
                }


            }
            catch (Exception ef)
            {
                response.resultCode = "Result Code: 0003";
                response.resultText = ef.Message + ". Contact Administrator";
            }

            return response;
        }

        private string getQueryResponse(NameValueCollection qData, string v)
        {
            string responseData;
            var webClient = new System.Net.WebClient();
            var response = webClient.UploadValues(v, qData);
            responseData = System.Text.Encoding.UTF8.GetString(response);
            return responseData;
        }
    }
}
