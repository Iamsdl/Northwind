﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.Models.Interfaces;
using NorthwindWeb.Models.ExceptionHandler;

namespace NorthwindWeb.Controllers
{
    [Authorize]
    /// <summary>
    /// Shippers Controller. For table Shippers
    /// </summary>
    public class ShippersController : Controller, IJsonTableFill
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Shippers
        ///Enter in  Shippers's details through CompanyName of Shippers
        public async Task<ActionResult> Index(string search = "")
        {
            return View(await db.Shippers.Where(x => x.CompanyName.Contains(search)).ToListAsync());
        }

        // GET: Shippers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ///take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        // GET: Shippers/Create
        [Authorize(Roles = "Employees, Admins")]
        ///Enter in the page Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shippers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        ///Create a new shipper which contain the next fields: ShipperID, CompanyName, Phone and it will be saved in database
        public async Task<ActionResult> Create([Bind(Include = "ShipperID,CompanyName,Phone")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                db.Shippers.Add(shippers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shippers);
        }

        // GET: Shippers/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        ///Enter in the page Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ///take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        // POST: Shippers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        ///Modify selected shipper and save it in database
        public async Task<ActionResult> Edit([Bind(Include = "ShipperID,CompanyName,Phone")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shippers);
        }

        // GET: Shippers/Delete/5
        [Authorize(Roles = "Admins")]
        ///Enter in the page Delete by ID the shipper
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ///take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        // POST: Shippers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        ///Delete the selected shipper
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ///take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            
            try 
            {
                db.Shippers.Remove(shippers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                string list = "";
                var orderid = db.Orders.Include(x => x.Shipper).Where(x => x.ShipVia == id).Select(x => new { x.OrderID });
                foreach (var i in orderid)
                {   //lopp in all OrderID
                    list = list + i.OrderID.ToString() + ", ";
                }
                throw new DeleteException("Nu poti sterge expeditorul deoarece contine angajati cu id-urile:\n" + list + "\n Pentru a putea sterge acest expeditor trebuie sterse comenzile si detaliile lor.");
            }
        }

        // GET: Shipper by Json
        /// send back a JsonDataTableFill as json with all the information that wee need to populate datatable
        public JsonResult JsonTableFill()
        {
            var shippers = db.Shippers.OrderBy(x => x.ShipperID);

            ///Select what wee need in table
            return Json(
                shippers.Select(x => new NorthwindWeb.Models.ServerClientCommunication.ShipperData
                {
                    ID = x.ShipperID,
                    CompanyName = x.CompanyName,
                    Phone = x.Phone
                })
                , JsonRequestBehavior.AllowGet);
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
