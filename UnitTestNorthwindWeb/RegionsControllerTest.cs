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
    public class RegionsControllerTest
    {
        //Arrange
        RegionsController _regionsControllerTest = new RegionsController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void RegionSampleTest()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("RegionsController", "RegionsController");
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void RegionReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _regionsControllerTest.Index("");


            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void RegionReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _regionsControllerTest.Index("");

            //Assert
            Assert.IsNotNull(result);


        }


        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task RegionReturnsDetails()
        {
            //Arrage
            Region regionTest = new Region() { RegionDescription = "Acasa" };
            //Act
            var result = await _regionsControllerTest.Details(regionTest.RegionID) ;
            

            //Assert
            Assert.IsNotNull(result);
        }



        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void RegionsCreateReturnsView()
        {
            //Arrange

            //Act
            var result = _regionsControllerTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task RegionCreate()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 4, RegionDescription = "Acasa" };
            //Act
            var expected = db.Regions.Count() + 1;
            await _regionsControllerTest.Create(regionTest);
            var actual = db.Regions.Count();
            var region = db.Regions.Where(c => c.RegionDescription == regionTest.RegionDescription );

            //Assert
            Assert.AreEqual(expected, actual);

            db.Regions.RemoveRange(region);
            db.SaveChanges();

        }



        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task RegionsDeleteReturnsView()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Acasa" };
            await _regionsControllerTest.Create(regionTest);

            //Act
            var result = _regionsControllerTest.Delete(regionTest.RegionID);

            //Assert
            Assert.IsNotNull(result);





            var region = db.Regions.Where(c => c.RegionDescription == regionTest.RegionDescription);
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task RegionDeleteDeletes()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Acasa" };
            await _regionsControllerTest.Create(regionTest);
            int expected = db.Shippers.Count() - 1;

            //Act
            await _regionsControllerTest.DeleteConfirmed(regionTest.RegionID);
            int actual = db.Shippers.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

       
        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task RegionEditEdits()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Aici" };
            await _regionsControllerTest.Create(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Added;

            var expectedRegion = db.Regions.Find(regionTest.RegionID);

            db.Dispose();
            regionTest.RegionDescription = "Acolo";
            db = new NorthwindModel();

            //Act
            await _regionsControllerTest.Edit(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Modified;
            var actualRegion = db.Regions.Find(regionTest.RegionID);

            //Assert
            Assert.AreEqual(expectedRegion, actualRegion);


            var region = db.Regions.Where(c => (c.RegionDescription == "Aici") || (c.RegionDescription == "Acolo"));
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }
    }
}