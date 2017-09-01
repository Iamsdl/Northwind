﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Order
{/// <summary>
/// The class used to store, join between the Orders, Customers and Shippers
/// </summary>
    public class OrderInfo
    {
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        public string CompanyName { get; set; }
        public string ShipperName { get; set; }
    }
}