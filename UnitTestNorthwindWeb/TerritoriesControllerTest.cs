﻿using NorthwindWeb.Models.ServerClientCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Threading.Tasks;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System;


namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class TerritoriesControllerTest
    {
        //Arrange
        TerritoriesController _territoriesControllerTest = new TerritoriesController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void TerritorySampleTest()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("TerritoriesController", "TerritoriesController");
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void TerritoryReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _territoriesControllerTest.Index();


            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void TerritoryReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _territoriesControllerTest.Index();

            //Assert
            Assert.IsNotNull(result);


        }


        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task TerritoryReturnsDetails()
        {
            //Arrage
            Territories territoryTest = new Territories() { TerritoryDescription = "Timis" };
            //Act
            var result = await _territoriesControllerTest.Details(territoryTest.TerritoryID);


            //Assert
            Assert.IsNotNull(result);
        }



        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void TerritoryCreateReturnsView()
        {
            //Arrange

            //Act
            //var result = _territoriesControllerTest.Create() as ViewResult;


            //Assert
            //Assert.IsNotNull(result);
        }
        
        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TerritoryCreate()
        {
            ////Arrange
            //Territories territoryTest = new Territories() {TerritoryID="50",TerritoryDescription = "Acasa" };
            ////Act
            //var expected = db.Territories.Count() + 1;
            //await _territoriesControllerTest.Create(territoryTest);
            //var actual = db.Territories.Count();
            //var territory = db.Territories.Where(t => t.TerritoryID == territoryTest.TerritoryID && t.TerritoryDescription == territoryTest.TerritoryDescription);

            ////Assert
            //Assert.AreEqual(expected, actual);

            //var territories = db.Territories.Where(t => t.TerritoryDescription.Contains("Acasa"));
            //db.Territories.RemoveRange(territories);
            //db.SaveChanges();

        }



        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TerritoryDeleteReturnsView()
        {
            ////Arrange
            //Territories territoryTest = new Territories() { TerritoryDescription = "Acasa" };
            //await _territoriesControllerTest.Create(territoryTest);

            ////Act
            //var result = _territoriesControllerTest.Delete(territoryTest.TerritoryID);

            ////Assert
            //Assert.IsNotNull(result);

            //var territory =db.Territories.Where(t => t.TerritoryDescription.Contains("Acasa"));
            //db.Territories.RemoveRange(territory);
            //db.SaveChanges();

        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task TerritoryDeleteDeletes()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 100,RegionDescription="test" };
            Territories territoryTest = new Territories() {TerritoryID="102", TerritoryDescription = "Acasa",Region=regionTest };
            await _territoriesControllerTest.Create(territoryTest,regionTest.RegionID);
            int expected = db.Territories.Count() - 1;

            //Act
            await _territoriesControllerTest.DeleteConfirmed(territoryTest.TerritoryID);
            int actual = db.Territories.Count();

            //Assert
            Assert.AreEqual(expected, actual);

            var regions = db.Regions.Where(t => t.RegionDescription.Contains(regionTest.RegionDescription));
            db.Regions.RemoveRange(regions);
            var territory = db.Territories.Where(t => (t.TerritoryDescription == "Acasa"));
            db.Territories.RemoveRange(territory);
            db.SaveChanges();

        }


        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async Task TerritoryEditEdits()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 100, RegionDescription = "test" };
            Territories territoryTest = new Territories() { TerritoryID="102", TerritoryDescription = "Aici",Region=regionTest };
            await _territoriesControllerTest.Create(territoryTest,regionTest.RegionID);
            db.Entry(territoryTest).State = System.Data.Entity.EntityState.Added;

            var expectedTerritory = db.Territories.Find(territoryTest.TerritoryID);

            db.Dispose();
            territoryTest.TerritoryDescription = "Acolo";
            db = new NorthwindModel();
            //Act
            await _territoriesControllerTest.Edit(territoryTest);
            db.Entry(territoryTest).State = System.Data.Entity.EntityState.Modified;
            var actualTerritory = db.Territories.Find(territoryTest.TerritoryID);

            //Assert
            Assert.AreEqual(expectedTerritory, actualTerritory);


            var territory = db.Territories.Where(t => (t.TerritoryDescription == "Aici") || (t.TerritoryDescription == "Acolo"));
            db.Territories.RemoveRange(territory);
            var regions = db.Regions.Where(t => t.RegionDescription.Contains(regionTest.RegionDescription));
            db.Regions.RemoveRange(regions);
            db.SaveChanges();
        }

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>//cu eroare
        [TestMethod]
        public void ShipperJsonTableFill()
        {
            ////Arrange
            //var controller = new TerritoriesController();
            //var territoryCount = db.Territories.Count();
            //int draw = 1;
            //int row = 20;

            ////Act
            //var jsonData = controller.JsonTableFill(draw, 0, row).Data as JsonDataTableObject;

            ////Assert
            //Assert.AreEqual(jsonData.draw, draw);
            //Assert.AreEqual(jsonData.recordsTotal, territoryCount);
            //Assert.IsTrue(jsonData.recordsFiltered <= territoryCount);
            //db.Dispose();
        }
    }
}
