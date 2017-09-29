﻿using System.Collections.Generic;

namespace NorthwindWeb.Core.ViewModels.Dashboard
{   /// <summary>
    /// The data model sent by the DashboardController to Home
    /// </summary>
    public class DashboardIndexData
    {   /// <summary>
        /// sales value
        /// </summary>
        public decimal TotalSalesValue { get; set; }
        /// <summary>
        /// Number Products Sold
        /// </summary>
        public int NumberProductsSold{get;set;}
        /// <summary>
        /// Number of Employees
        /// </summary>
        public int NumberEmployees { get; set; }
        /// <summary>
        /// Number of Customers
        /// </summary>
        public int NumberCustomers { get; set; }
        /// <summary>
        /// Sales Per Quarter
        /// </summary>
        public IEnumerable<DashboardMorrisBar> SalesPerQuarter { get; set; }
        /// <summary>
        /// Last Ten Orders
        /// </summary>
        public IEnumerable<LastTenOrders> LastTenOrders { get; set; }

    }
}