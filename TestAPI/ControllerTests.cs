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
using Open.GI.hypermart.Controllers;
using TestAPI.Moqs;



namespace TestAPI
{
    /// <summary>
    /// API Controller Tests
    /// </summary>
    [TestFixture]
    public class APIControllerTests
    {
        public void Setup()
        {
            //mockDB = new Mock<IHypermartContext>();
            //mockDB.Setup(db => db.Products).Returns(Products);
           
        }

        [Test]
        public void CanListAllApps()
        {
            fakeCustomers = new List<Product>()
            {
                new Product(){ID =1,Description="d1",Files = null,Lead="l1",Screenshots = null,Tagline="tl1",Title = "title1"},
                new Product(){ID =2,Description="d2",Files = null,Lead="l2",Screenshots = null,Tagline="tl2",Title = "title2"},
                new Product(){ID =3,Description="d3",Files = null,Lead="l3",Screenshots = null,Tagline="tl3",Title = "title3"},
            };


            var mockedContext = new Mock<Open.GI.hypermart.DAL.HypermartContext>();
            mockedContext.Setup(c => c.Products).ReturnsDbSet(fakeCustomers);
            var p = mockedContext.Object.Products;
            
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockedContext.Object);
            
            var AllProducts = CUT.GetAllProducts();
            
            Assert.AreEqual(3, AllProducts.Count(), "Expected 3 products");

        }

        List<Product> fakeCustomers;


 


        [Test]
        public void CanAddNewProduct()
        {
            
            var ProductToAdd = new Product() { ID = 4, Description = "d4", Files = null, Lead = "l4", Screenshots = null, Tagline = "tl4", Title = "title4" };
            var ExpectedProductDTO = new Open.GI.hypermart.DataTransformationObjects.ProductDTO() 
            {
                Description = ProductToAdd.Description,
                ID = ProductToAdd.ID,
                Lead = ProductToAdd.Lead,
                Tagline = ProductToAdd.Tagline,
                Title = ProductToAdd.Title 

            
            };

            var mockProspect = new Mock<DbSet<Product>>() ;
            mockProspect.Object.Add(ProductToAdd);

            var MockDbContext = EntityFrameworkMockHelper.GetMockContext<HypermartContext>();
            MockDbContext.Object.Products.Add(new Product() { ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" });
            MockDbContext.Object.Products.Add(new Product() { ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" });
            MockDbContext.Object.Products.Add(new Product() { ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" });

            MockDbContext.Setup(m => m.Set<Product>()).Returns(mockProspect.Object );
            MockDbContext.Setup(m=> m.Products.Add(It.IsAny<Product>())).Returns(ProductToAdd);
             
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext.Object);
           
            var res = CUT.AddProduct(ProductToAdd);
            Assert.AreEqual(ExpectedProductDTO.ID, res.ID, "Expected prouct returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Description, res.Description, "Expected prouct returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Lead, res.Lead, "Expected prouct returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Tagline, res.Tagline, "Expected prouct returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Title, res.Title, "Expected prouct returned from Add Product call does not match expected product");

        }

        private void  dataAdd(Product obj)
        {
            fakeCustomers.Add(obj);
             

        }
    
    }
}
