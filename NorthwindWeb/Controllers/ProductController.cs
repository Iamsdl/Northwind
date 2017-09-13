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
using PagedList;
using System.Web.Helpers;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.ExceptionHandler;


namespace NorthwindWeb.Controllers
{

    /// <summary>
    /// ProductController is controller that we use to show all product for Admins, Manager, Employees
    /// </summary>
    [Authorize(Roles = "Admins, Manager, Employees")]
    public class ProductController : Controller, IJsonTableFillServerSide
    {
        private NorthwindModel db = new NorthwindModel();


        /// <summary>
        /// Displays a page containing a datatable with all the products in the database.
        /// </summary>
        /// <param name="category">Filters the products and only shows the ones containing this category.</param>
        /// <returns>Product index view</returns>
        public ActionResult Index(string category = "")
        {
            //category from browser adress is used also in JsonTableFill action
            ViewBag.category = category;
            return View();
        }

        /// <summary>
        /// Displays a page containing all the information about one product
        /// </summary>
        /// <param name="id">The id of the product that is going to be created.</param>
        /// <returns>Product details view</returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        /// <summary>
        /// Displays a page containing a form required to add a new product in the database.
        /// </summary>
        /// <returns>Product create view</returns>
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        /// <summary>
        /// Creates a new product and adds it to the database.
        /// </summary>
        /// <param name="products">The products entity that will be added.</param>
        /// <returns>Product index view</returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        /// <summary>
        /// Displays a page containing a form required to edit a product.
        /// </summary>
        /// <param name="id">The id of the product that is going to be edited.</param>
        /// <returns>Product edit view</returns>
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        /// <summary>
        /// Updates the information of a product in the database.
        /// </summary>
        /// <param name="products">The product entity with the updated information.</param>
        /// <returns>Product index view</returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete operation.
        /// </summary>
        /// <param name="id">The id of the product that is going to be deleted.</param>
        /// <returns>Product delete view</returns>
        //TODO Delete from related tables
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);

        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="id">The id of the product that is going to be deleted.</param>
        /// <returns>Product index view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Products products = await db.Products.FindAsync(id);
                db.Products.Remove(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                string listOrders = "";
                var ordersWhitProductID = db.Order_Details.Include(s => s.Order).Include(s => s.Product).Where(s => s.ProductID == id).Select(s => new { s.OrderID });
                foreach (var i in ordersWhitProductID)
                {
                    listOrders = i.OrderID + ", ";
                }
                throw new DeleteException("Acest produs nu a putut fi sters deoarece se afla pe urmatoarele comenzi: " + listOrders + " .Ia in considerare si varianta de a-l face indisponibil");
            }
        }


        /// <summary>
        /// Send back a JsonDataTableObject as json with all the information that we need to populate datatable
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asincron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>JsonDataTableObject</returns>
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;
            string category = "";
            try
            {
                category = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["category"] ?? "";
            }
            catch (NullReferenceException) { }

            string search = "";
            try
            {
                search = Request.QueryString["search[value]"] ?? "";
            }
            catch (NullReferenceException) { }


            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            try
            {
                if (Request.QueryString["order[0][column]"] != null)
                {
                    try
                    {
                        sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                    }
                    catch (NullReferenceException) { }
                }
            }
            catch (NullReferenceException) { }

            try
            {
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    try
                    {
                        sortDirection = Request.QueryString["order[0][dir]"];
                    }
                    catch (NullReferenceException) { }
                }
            }
            catch (NullReferenceException) { }

            //list of product that contain "search"
            var list = db.Products.Include(p => p.Category).Include(p => p.Supplier)
                .Where(p => (p.ProductName.Contains(search) || p.ProductID.ToString().Contains(search)
                || p.Discontinued.ToString().Contains(search) || p.Supplier.CompanyName.Contains(search)) && p.Category.CategoryName.Contains(category));

            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ProductName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ProductName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitPrice);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitPrice);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsInStock);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsInStock);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsOnOrder);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsOnOrder);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ReorderLevel);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ReorderLevel);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Discontinued);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Discontinued);
                    }
                    break;
            }

            //objet that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = db.Products.Count(),
                data = list.Skip(start).Take(length).Select(x => new
                {
                    ID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.UnitPrice,
                    InStock = x.UnitsInStock,
                    OnOrders = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                }),
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
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
