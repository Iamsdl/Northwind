﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ShipperControllerTest
    {
        //Arrange
        ShippersController _shippersControllerTest = new ShippersController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void SampleTestShipper()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("ShippersController", "ShippersController");
        }


        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ShipperReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _shippersControllerTest.Index("");

            //Assert

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ShipperReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _shippersControllerTest.Index("");

            //Assert

            Assert.IsNotNull(result);


        }
        

        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task ShipperReturnsDetails()
        {
            //Arrage
            Shippers shipperTest = new Shippers() { CompanyName = "FAN Courier" };
            //Act
            var result = await _shippersControllerTest.Details(shipperTest.ShipperID) ;
            

            //Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void ShipperCreateReturnsView()
        {
            //Arrange

            //Act
            var result = _shippersControllerTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ShipperCreate()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { ShipperID=4, CompanyName="Nero", Phone="0240-555-555" };
            //Act
            var expected = db.Shippers.Count() + 1;
            await _shippersControllerTest.Create(shipperTest);
            var actual = db.Shippers.Count();
            var shipper = db.Shippers.Where(c => c.CompanyName == shipperTest.CompanyName && c.Phone == shipperTest.Phone);

            //Assert
            Assert.AreEqual(expected, actual);

            db.Shippers.RemoveRange(shipper);
            db.SaveChanges();

        }

        

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ShipperDeleteReturnsView()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Nero", Phone = "0240-555-555" };
            await _shippersControllerTest.Create(shipperTest);

            //Act
            var result = _shippersControllerTest.Delete(shipperTest.ShipperID);

            //Assert
            Assert.IsNotNull(result);





            var category = db.Shippers.Where(c => c.CompanyName == shipperTest.CompanyName && c.Phone == shipperTest.Phone);
            db.Shippers.RemoveRange(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task ShipperDeleteDeletes()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Nero", Phone = "0240-555-555" };
            await _shippersControllerTest.Create(shipperTest);
            int expected = db.Shippers.Count() - 1;

            //Act
            await _shippersControllerTest.DeleteConfirmed(shipperTest.ShipperID);
            int actual = db.Shippers.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        

        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task ShipperEditEdits()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Express", Phone = "0240-111-111" };
            await _shippersControllerTest.Create(shipperTest);
            db.Entry(shipperTest).State = System.Data.Entity.EntityState.Added;

            var expectedShipper = db.Shippers.Find(shipperTest.ShipperID);

            db.Dispose();
            shipperTest.CompanyName = "Nero Express";
            shipperTest.Phone = "0240-222-222";
            db = new NorthwindModel();

            //Act
            await _shippersControllerTest.Edit(shipperTest);
            db.Entry(shipperTest).State = System.Data.Entity.EntityState.Modified;
            var actualShipper = db.Shippers.Find(shipperTest.ShipperID);

            //Assert
            Assert.AreEqual(expectedShipper, actualShipper);


            var shipper = db.Shippers.Where(c => (c.CompanyName == "Express" && c.Phone == "0240-111-111") || (c.CompanyName == "Nero Express" && c.Phone == "0240-222-222"));
            db.Shippers.RemoveRange(shipper);
            db.SaveChanges();
        }
    }
}