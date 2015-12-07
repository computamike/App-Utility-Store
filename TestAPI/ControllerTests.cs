using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Open.GI.hypermart;
using Open.GI.hypermart.Models;
using Open.GI.hypermart.DAL;
using Moq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Moq.Language.Flow;
using Moq.Language;
namespace TestAPI
{
    [TestFixture]
    public class ControllerTests
    {

 
 

        public void Setup()
        {




            //mockDB = new Mock<IHypermartContext>();
            //mockDB.Setup(db => db.Products).Returns(Products);
           
        }



        [Test]
        public void CanListAllApps()
        {
            var fakeCustomers = new Product[]
            {
                new Product(){ID =1,Description="d1",Files = null,Lead="l1",Screenshots = null,Tagline="tl1",Title = "title1"},
                new Product(){ID =2,Description="d2",Files = null,Lead="l2",Screenshots = null,Tagline="tl2",Title = "title2"},
                new Product(){ID =3,Description="d3",Files = null,Lead="l3",Screenshots = null,Tagline="tl3",Title = "title3"},
            };


            var mockedContext = new Mock<Open.GI.hypermart.DAL.HypermartContext>();
            mockedContext.Setup(c => c.Products).ReturnsDbSet(fakeCustomers);
            var p = mockedContext.Object.Products;
            
            Open.GI.hypermart.Controllers.StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockedContext.Object);
            
            var AllProducts = CUT.GetAllProducts();
            
            Assert.AreEqual(3, AllProducts.Count(), "Expected 3 products");

        }
    }
}
