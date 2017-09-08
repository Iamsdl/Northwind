﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using System.Web.Script.Serialization;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Used from enter page from the site (public site not admin site).
    /// </summary>
    public class HomeController : Controller
    {

        NorthwindModel _northwindDatabase = new NorthwindModel();

        /// <summary>
        /// First page in the site.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.SiteName = "Northwind Phone Shop";

            return View("Index");
        }

        /// <summary>
        /// Used to construct the menu.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var productsCategories = _northwindDatabase.Categories;
            List<string> listOfCategories = new List<string>();
            foreach (var item in productsCategories)
            {
                string categoryName = item.CategoryName;
                listOfCategories.Add(categoryName);
            }

            return View(listOfCategories);
        }


        /// <summary>
        /// If you see this please delete it (public string CreateJsonTableObject())
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admins")]
        public string CreateJsonTableObject(string path = "d:\\Table")
        {
            var db = new NorthwindModel();

            System.IO.File.WriteAllText(path + "\\categoies.json", new JavaScriptSerializer().Serialize(
                db.Categories.Select(x => new
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                }
                )));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Customers.Select(x => new
                {
                    CustomerID = x.CustomerID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    Phone = x.Phone,
                    Fax = x.Fax,
                }
                )));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Employees.Select(x => new
                {
                    EmployeeID = x.EmployeeID,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    TitleOfCourtesy = x.TitleOfCourtesy,
                    BirthDate = x.BirthDate,
                    HireDate = x.HireDate,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    HomePhone = x.HomePhone,
                    Extension = x.Extension,
                    Photo = x.Photo,
                    Notes = x.Notes,
                    ReportsTo = x.ReportsTo
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Territories.Select(x => new
                {
                    TerritoryID = x.TerritoryID,
                    TerritoryDescription = x.TerritoryDescription,
                    RegionID = x.RegionID
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Suppliers.Select(x => new
                {
                    SupplierID = x.SupplierID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    Phone = x.Phone,
                    Fax = x.Fax,
                    HomePage = x.HomePage
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Shippers.Select(x => new
                {
                    ShipperID = x.ShipperID,
                    CompanyName = x.CompanyName,
                    Phone = x.Phone
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Regions.Select(x => new
                {
                    RegionID = x.RegionID,
                    RegionDescription = x.RegionDescription
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Orders.Select(x => new
                {
                    OrderID = x.OrderID,
                    CustomerID = x.CustomerID,
                    EmployeeID = x.EmployeeID,
                    OrderDate = x.OrderDate,
                    RequiredDate = x.RequiredDate,
                    ShippedDate = x.ShippedDate,
                    ShipVia = x.ShipVia,
                    Freight = x.Freight,
                    ShipName = x.ShipName,
                    ShipAddress = x.ShipAddress,
                    ShipCity = x.ShipCity,
                    ShipRegion = x.ShipRegion,
                    ShipPostalCode = x.ShipPostalCode,
                    ShipCountry = x.ShipCountry,
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Products.Select(x => new
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    SupplierID = x.SupplierID,
                    CategoryID = x.CategoryID,
                    QuantityPerUnit = x.QuantityPerUnit,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    UnitsOnOrder = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                })));
            System.IO.File.WriteAllText(path + "\\products.json", new JavaScriptSerializer().Serialize(
                db.Order_Details.Select(x => new
                {
                    OrderID = x.OrderID,
                    ProductID = x.ProductID,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    Discount = x.Discount
                })));

            db.Dispose();

            return "Succes! \n Baza de date Northwind a fost salvata ca json in " + path;
        }

    }
}