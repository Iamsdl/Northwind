﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Dashboard;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace NorthwindWeb.Controllers
{   
    [Authorize(Roles = "Admins")]
    public class DashboardController : Controller
    {
        private NorthwindModel db = new NorthwindModel();



        public ActionResult Index1()
        {
            DashboardIndexData viewModel = new DashboardIndexData();

            viewModel.TotalSalesValue = TotalSalesValue();
            viewModel.NumberProductsSold = NumberProductsSold();
            viewModel.NumberEmployees = NumberEmployees();
            viewModel.NumberCustomers = NumberCustomers();
            viewModel.Tabel = Table();
            viewModel.LastTen = LastTen();

            return View(viewModel);
        }

        public ActionResult Search(string search)
        {
            List<LocateSearch> list = new List<LocateSearch>();
            if (!String.IsNullOrEmpty(search))
            {
                var category = db.Categories;
                foreach (var item in category)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.CategoryID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "CategoryID: " +Convert.ToString(item.CategoryID);
                        x.Controller = "Categories";
                        list.Add(x);
                    }
                    else if(item.CategoryName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "CategoryName: " + item.CategoryName;
                        x.Controller = "Categories";
                        list.Add(x);
                    }
                    else if(item.Description.Contains(search))
                    {
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "Description: " + item.Description;
                        x.Controller = "Categories";
                        list.Add(x);
                    }

                }
                var suppliers = db.Suppliers;
                foreach (var item in suppliers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.SupplierID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "CategoryID: " + Convert.ToString(item.SupplierID);
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }
                    else if (item.ContactName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "ContactName: " + item.ContactName;
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }

                }
                var product = db.Products;
                foreach (var item in product)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.ProductID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.ProductID);
                        x.WhereFound = "ProductID: " + Convert.ToString(item.ProductID);
                        x.Controller = "Product";
                        list.Add(x);
                    }
                    else if (item.ProductName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.ProductID);
                        x.WhereFound = "ProductName: " + item.ProductName;
                        x.Controller = "Product";
                        list.Add(x);
                    }
                 

                }
                var order = db.Orders;
                foreach (var item in order)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.OrderID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "OrderID: " + Convert.ToString(item.OrderID);
                        x.Controller = "Orders";
                        list.Add(x);
                    }
                    else if (Convert.ToString(item.OrderDate).Contains(search))
                    {
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "OrderDate: " + Convert.ToString(item.OrderDate);
                        x.Controller = "Orders";
                        list.Add(x);
                    }
                    else if (item.ShipName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "ShipName: " + item.ShipName;
                        x.Controller = "Orders";
                        list.Add(x);
                    }

                }
                var customers = db.Customers;
                foreach (var item in customers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.CustomerID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "CustomerID: " + Convert.ToString(item.CustomerID);
                        x.Controller = "Customers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Customers";
                        list.Add(x);
                    }
                    else if (item.ContactName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "ContactName: " + item.ContactName;
                        x.Controller = "Customers";
                        list.Add(x);
                    }

                }
                var region = db.Regions;
                foreach (var item in region)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.RegionID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.RegionID);
                        x.WhereFound = "RegionID: " + Convert.ToString(item.RegionID);
                        x.Controller = "Regions";
                        list.Add(x);
                    }
                    else if (item.RegionDescription.Contains(search))
                    {
                        x.ID = Convert.ToString(item.RegionID);
                        x.WhereFound = "RegionDescription: " + item.RegionDescription;
                        x.Controller = "Regions";
                        list.Add(x);
                    }
                    

                }
                var employees = db.Employees;
                foreach (var item in employees)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.EmployeeID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "EmployeeID: " + Convert.ToString(item.EmployeeID);
                        x.Controller = "Employees";
                        list.Add(x);
                    }
                    else if (item.LastName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "LastName: " + item.LastName;
                        x.Controller = "Employees";
                        list.Add(x);
                    }
                    else if (item.FirstName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "FirstName: " + item.FirstName;
                        x.Controller = "Employees";
                        list.Add(x);
                    }

                }
                var shippers = db.Shippers;
                foreach (var item in shippers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.ShipperID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.ShipperID);
                        x.WhereFound = "ShipperID: " + Convert.ToString(item.ShipperID);
                        x.Controller = "Shippers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.Contains(search))
                    {
                        x.ID = Convert.ToString(item.ShipperID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Shippers";
                        list.Add(x);
                    }


                }
                var territories = db.Territories;
                foreach (var item in territories)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.TerritoryID).Contains(search))
                    {
                        x.ID = Convert.ToString(item.TerritoryID);
                        x.WhereFound = "TerritoryID: " + Convert.ToString(item.TerritoryID);
                        x.Controller = "Territories";
                        list.Add(x);
                    }
                    else if (item.TerritoryDescription.Contains(search))
                    {
                        x.ID = Convert.ToString(item.TerritoryID);
                        x.WhereFound = "TerritoryDescription: " + item.TerritoryDescription;
                        x.Controller = "Territories";
                        list.Add(x);
                    }


                }

            }
            if (list.Count == 0)
            {
                LocateSearch x = new LocateSearch();
                x.WhereFound = "Not Found";
                x.Controller = "Dashboard";
            }
            
            return View(list);


           
        }

        public ActionResult Graph1()
        {
            List<DashboardGraph1> list = new List<DashboardGraph1>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            foreach (var item in salesbyyear)
            {
                int ok = 0;
                foreach (var i in list)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(item.OrderDate).Year)
                    {
                        i.Sales += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                        ok = 1;
                        break;
                    }
                }
                if (ok == 0)
                {
                    DashboardGraph1 x = new DashboardGraph1();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    x.Sales = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                    list.Add(x);
                }
            }
            foreach (var i in list)
            {
                i.Sales = decimal.Round(i.Sales, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph2()
        {


            return Json(Table(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph3()
        {
            List<DashboardGraph3> list = new List<DashboardGraph3>();
            var salesbyyear = from od in db.Order_Details
                              join p in db.Products on od.ProductID equals p.ProductID
                              join c in db.Categories on p.CategoryID equals c.CategoryID
                              select new { od.UnitPrice, od.Quantity, od.Discount, c.CategoryName };
            var categori = from c in db.Categories
                           select new { c.CategoryName };
            foreach (var item in categori)
            {
                DashboardGraph3 x = new DashboardGraph3();
                x.label = item.CategoryName;
                x.value = 0;
                list.Add(x);
            }
            foreach (var item in salesbyyear)
            {

                foreach (var i in list)
                {
                    if (i.label == item.CategoryName)
                    {
                        i.value += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));

                        break;
                    }
                }

            }
            foreach (var i in list)
            {
                i.value = decimal.Round(i.value, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private List<DashboardGraph2> Table()
        {
            List<DashboardGraph2> list = new List<DashboardGraph2>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            foreach (var item in salesbyyear)
            {
                int t;
                if (Convert.ToDateTime(item.OrderDate).Month <= 4) { t = 1; }
                else if (Convert.ToDateTime(item.OrderDate).Month <= 8) { t = 2; }
                else { t = 3; }
                int ok = 0;
                foreach (var i in list)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(item.OrderDate).Year)
                    {
                        if (t == 1) { i.a += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else if (t == 2) { i.b += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else { i.c += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }

                        ok = 1;
                        break;
                    }
                }
                if (ok == 0)
                {
                    DashboardGraph2 x = new DashboardGraph2();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    if (t == 1) { x.a = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else if (t == 2) { x.b = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else { x.c = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    list.Add(x);
                }
            }
            foreach (var i in list)
            {
                {
                    i.a = decimal.Round(i.a, 2);
                    i.b = decimal.Round(i.b, 2);
                    i.c = decimal.Round(i.c, 2);
                }
            }
            return list;
        }

        private decimal TotalSalesValue()
        {
            decimal d = 0;
            var sales = db.Order_Details;
            foreach (var item in sales)
            {
                d += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));

            }
            d = decimal.Round(d, 2);
            return d;
        }

        private int NumberProductsSold()
        {
            int p = 0;
            var product = db.Order_Details;
            foreach (var item in product)
            {
                p += item.Quantity;

            }
            return p;
        }

        private int NumberEmployees()
        {
            int e = db.Employees.Count();
            return e;
        }

        private int NumberCustomers()
        {
            int c = db.Customers.Count();
            return c;
        }

        private List<Last10Orders> LastTen()
        {
            List<Last10Orders> list = new List<Last10Orders>();
            var order = (from o in db.Orders
                         select new { o.OrderID, o.OrderDate }).OrderByDescending(o => o.OrderDate).Take(10);
            foreach (var item in order)
            {
                Last10Orders x = new Last10Orders();
                x.OrderID = item.OrderID;
                var y = DateTime.Now - Convert.ToDateTime(item.OrderDate);
                if (y.TotalMinutes <= 60) { x.Ago = "Acum " + Convert.ToString(y.TotalMinutes) + " Minute"; }
                else if (y.TotalHours <= 24) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Ore"; }
                else if (y.TotalDays <= 30) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Zile"; }
                else { x.Ago = "Cu mai mult de o luna in urma"; }
                list.Add(x);
            }
            return list;

        }


    }

}