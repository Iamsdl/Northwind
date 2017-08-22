﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;
using PagedList;


namespace NorthwindWeb.Controllers
{


    public class OrderController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        public ActionResult Home1(int? id, int? pid, int? page, string search, string currentFilter)
        {
            var viewModel = new OrderIndexData();
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            viewModel.Order = Comenzii(search);
            viewModel.Comanda = BigComanda();

            viewModel.Order10 = Lista();
            if (id == 0) { id = null; }
            if (id != null)
            {
                ViewBag.OrderID = id.Value;
                viewModel.Order_Detail = Detali(ViewBag.OrderID);
                

            }
            if (pid == 0) { pid = null; }
            if (pid != null)
            {
                ViewBag.ProductID = pid.Value;

                viewModel.Product = ProdCateg(ViewBag.ProductID);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            viewModel.page = viewModel.Order.ToPagedList(pageNumber, pageSize);
            viewModel.Order = viewModel.Order.ToPagedList(pageNumber, pageSize);
            return View(viewModel);

        }

        public List<Order10> Lista()
        {
            var order10 = (from o in db.Orders
                           join od in db.Order_Details on o.OrderID equals od.OrderID
                           group od by o.OrderID into x
                           select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                              .OrderByDescending(x => x.Cost)
                              .Take(10);
            ;
            List<Order10> list = new List<Order10>();

            foreach (var item in order10)
            {
                Order10 x = new Order10();

                x.OrderID = item.OrderID;
                x.Cost = decimal.Round(item.Cost, 2);
                list.Add(x);

            }
            return list;
        }

        public List<Comanda> Comenzii(string search)
        {
            var order = (from o in db.Orders
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         join s in db.Shippers on o.ShipVia equals s.ShipperID
                         select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName })
                       ;
            if (!String.IsNullOrEmpty(search))
            {
                int i;
                if (int.TryParse(search, out  i))
                {
                    order = order.Where(s => s.OrderID == i);
                }
                else
                {
                    order = order.Where(s => s.ShipperName.Contains(search));
                }
            }
            order.OrderBy(i => i.OrderID);
            List<Comanda> comenzi = new List<Comanda>();

            foreach (var item in order)
            {
                Comanda x = new Comanda();

                x.OrderID = item.OrderID;
                DateTime t = Convert.ToDateTime(item.OrderDate);
                x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                x.CompanyName = item.CompanyName;
                x.ShipperName = item.ShipperName;
                comenzi.Add(x);

            }
            return comenzi;
        }

        public BigOrder BigComanda()
        {
            var order = (from o in db.Orders
                         join od in db.Order_Details on o.OrderID equals od.OrderID
                         group od by o.OrderID into x
                         select new { OrderID = x.Key, max = x.Sum(o=>o.Quantity) })
                                  .OrderByDescending(x => x.max)
                                  .Take(1);
            BigOrder s=new BigOrder();
            foreach (var item in order)
            {
                s.OrderID = item.OrderID;
                s.Produse = item.max;
            }
            return s;
        }

        public List<OrderProduct> Detali(int id)
        {
            var detali = (from o in db.Order_Details
                          .Where(x => x.OrderID == id)
                          join p in db.Products on o.ProductID equals p.ProductID
                          select new { p.ProductName, o.UnitPrice,o.Quantity,o.Discount,o.ProductID })
                              
            ;
            List<OrderProduct> list = new List<OrderProduct>();

            foreach (var item in detali)
            {
                OrderProduct x = new OrderProduct();
                x.ProductID = item.ProductID;
                x.ProductName = item.ProductName;
                x.UnitPrice = item.UnitPrice;
                x.Quantity = item.Quantity;
                x.Discount = item.Discount;
                list.Add(x);

            }
            return list;
        }

        public List<ProductCategory> ProdCateg(int id)
        {
            var detali = (from p in db.Products
                          .Where(x => x.ProductID == id)
                          join c in db.Categories on p.CategoryID equals c.CategoryID
                          select new { p.ProductName, c.CategoryName,p.UnitsInStock })

            ;
            List<ProductCategory> list = new List<ProductCategory>();

            foreach (var item in detali)
            {
                ProductCategory x = new ProductCategory();
                x.ProductName = item.ProductName;
                x.CategoryName = item.CategoryName;
                x.UnitsInStock = item.UnitsInStock;
                
                list.Add(x);

            }
            return list;
        }
    }
}
