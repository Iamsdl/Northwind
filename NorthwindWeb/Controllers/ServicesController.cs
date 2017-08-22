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
    /// <summary>
    /// 
    /// </summary>
    public class ServicesController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var viewModel = new ServicesIndex();

            //take the names of first 4 products
            viewModel.top4name = (from p in db.Products
                                  orderby p.ProductID
                                  select p.ProductName).Take(4);

            //take first 4 products
            var products = (from p in db.Products
                           join c in db.Categories on p.CategoryID equals c.CategoryID
                           join s in db.Suppliers on p.SupplierID equals s.SupplierID
                           select new
                           {
                               p.ProductName,
                               s.CompanyName,
                               c.CategoryName,
                               p.QuantityPerUnit,
                               p.UnitPrice,
                               p.UnitsInStock,
                               p.UnitsOnOrder,
                           }).Take(4);

            List<ProductServices> listOfProducts = new List<ProductServices>();

            //lopp in first 4 products 
            foreach (var item in products)
            {
                ProductServices x = new ProductServices();

                x.ProductName = item.ProductName;
                x.CategoryName = item.CategoryName;
                x.CompanyName = item.CompanyName;
                x.QuantityPerUnit = item.QuantityPerUnit;
                x.UnitPrice = item.UnitPrice;
                x.UnitsInStock = item.UnitsInStock;
                x.UnitsOnOrder = item.UnitsOnOrder;

                listOfProducts.Add(x);

            }
            viewModel.top4products = listOfProducts;


            //take last 3 products
            var productsOrderByDesc = (from p in db.Products
                           join c in db.Categories on p.CategoryID equals c.CategoryID
                           join s in db.Suppliers on p.SupplierID equals s.SupplierID
                           orderby p.ProductID descending
                           select new
                           {
                               p.ProductName,
                               p.ProductID,
                               s.CompanyName,
                               c.CategoryName,
                               p.QuantityPerUnit,
                               p.UnitPrice,
                               p.UnitsInStock,
                               p.UnitsOnOrder,
                           }).Take(3);

            List<ProductServices> listOfProductsOrderByDesc = new List<ProductServices>();

            //loop in last 3 products
            foreach (var item2 in productsOrderByDesc)
            {
                ProductServices y = new ProductServices();

                y.ProductName = item2.ProductName;
                y.ProductID = item2.ProductID;
                y.CategoryName = item2.CategoryName;
                y.CompanyName = item2.CompanyName;
                y.QuantityPerUnit = item2.QuantityPerUnit;
                y.UnitPrice = item2.UnitPrice;
                y.UnitsInStock = item2.UnitsInStock;
                y.UnitsOnOrder = item2.UnitsOnOrder;

                listOfProductsOrderByDesc.Add(y);

            }
            viewModel.last3 = listOfProductsOrderByDesc;


            return View(viewModel);
        }
	}
}