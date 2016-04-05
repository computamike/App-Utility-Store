using Moq;
using NUnit.Framework;
using Open.GI.hypermart.Controllers;
using Open.GI.hypermart.Controllers.API;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TitaniumBunker.PhonySession;

// Notes : Some of these tests are being completed using https://msdn.microsoft.com/en-gb/data/dn314429.aspx as a guide


// Testing strategy
// ------- --------
// 
// Looking at the possible methods of testing a Controller / DB Context there seems to be three mechanism for doing this.
//
// 1. InMemoryDbSet (using an InMemoryDbSet.cs from https://github.com/a-h/FakeDbSet/blob/master/FakeDbSet/InMemoryDbSet.cs)
// 2. Using MOQ to moq the DB set
// 3. Using a LocalDB to allow integration testing against a 'real' database
//
// This is challenging for practitioners - what level of testing is acceptable ? Therefore this testing project will contain
// 3 test classes, covering these different testing approaches.
//
//
// Notes : 
// the Products controller allows the user to post screen shots and files through the request object.  This allows a single page to 
// support posting multiple files to the controller.  This system doesn't work well under a 
// InMemoryDBSet test - instead a MOQ test should be used.
//
// 


/// <summary>
/// 
/// </summary>
namespace TestAPI
{
    /// <summary>
    /// API Controller Tests
    /// </summary>
    [TestFixture]
    public class APIControllerTests
    {
        //
        //TODO : checking for equality between 2 instances of an object can be done by overriding the equality operator.  This is more convenient that checking each property.
        //
        List<Product> fakeProducts;
        MockCntext MockDbContext;
        List<Platform> platforms;

        private IQueryable<Product> Products;
        private Mock<IHypermartContext> mockEFContext;
        private Mock<IDbSet<Product>> mockSet = new Mock<IDbSet<Product>>();
        private Mock<IDbSet<File>> mockdbSetFiles = new Mock<IDbSet<File>>();
        private IQueryable<File> mockDataListFiles;
        [SetUp]
        public void Setup()
        {
            // Add platforms - at this stage add generic (such as WINDOWS) and specific (such as WINDOWS 32 BIT) - this might change
            platforms = new List<Platform>
            {
                new Platform{ID = "Windows",Platform1 = "Windows"},
                new Platform{ID = "Win_32",Platform1 = "Windows (32 bit)"},
                new Platform{ID = "Win_64",Platform1 = "Windows (64 bit)"},
                                
                new Platform{ID = "Win_8_32",Platform1 = "Windows 8 (32 bit)"},
                new Platform{ID = "Win_8_64",Platform1 = "Windows 8 (64 bit)"},
                                
                new Platform{ID = "Win_10_32",Platform1 = "Windows 10 (32 bit)"},
                new Platform{ID = "Win_10_64",Platform1 = "Windows 10 (64 bit)"},
                
                new Platform{ID = "Linux",Platform1 = "Linux"},
                new Platform{ID = "Linux32",Platform1 = "Linux (32 bit)"},
                new Platform{ID = "Linux64",Platform1 = "Linux (32 bit)"},
               
                new Platform{ID = "Apple",Platform1 = "Apple"}


            };


            //MockDbContext = new MockCntext();
            mockEFContext = new Mock<IHypermartContext>();

            // Set up In Memory lists to emulate data in the database. 
            Products = new List<Product> 
            {
                new Product{ ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" },
                new Product{ ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" },
                new Product{ ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" }

            }.AsQueryable();


            // Set up the DB Set : 

     




            // Set up the DB Sets
            mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.AsQueryable().Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.AsQueryable().Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.AsQueryable().ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => Products.GetEnumerator());

            var res1 = mockSet.Object;
            var res2 = mockSet.Object;





            mockDataListFiles = new List<File> 
            { 
                new File { FileName  = "BBB" }, 
                new File { FileName  = "ZZZ" }, 
                new File { FileName  = "AAA" }, 
            }.AsQueryable();


            mockdbSetFiles = new Mock<IDbSet<File>>();
            mockdbSetFiles.As<IQueryable<File>>().Setup(m => m.Provider).Returns(mockDataListFiles.Provider);
            mockdbSetFiles.As<IQueryable<File>>().Setup(m => m.Expression).Returns(mockDataListFiles.Expression);
            mockdbSetFiles.As<IQueryable<File>>().Setup(m => m.ElementType).Returns(mockDataListFiles.ElementType);
            mockdbSetFiles.As<IQueryable<File>>().Setup(m => m.GetEnumerator()).Returns(mockDataListFiles.GetEnumerator());

            // Set up context to return these DB Sets
            mockEFContext.Setup(x => x.Products).Returns(mockSet.Object );
            mockEFContext.Setup(x => x.Files).Returns(mockdbSetFiles.Object);



            //mockEFContext.Setup(x => x.Files.Add(It.IsAny<File>())).Returns(new File { ID = 1, Product = new Product { ID =1,Description = "FooBar App"}, FileName = "FooBar.exe" });
            //mockEFContext.Setup(x => x.Products.Find(1)).Returns(Products.FirstOrDefault(d=> d.ID == 1));

        
        
        }

        [Test]
        public void CanListAllProducts()
        {
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockEFContext.Object);
            var s = mockSet.Object;
            var t = mockSet.Object;
            var u = mockEFContext.Object.Products;
            var v = mockEFContext.Object.Products;

            var AllProducts = CUT.GetAllProducts();
            
            Assert.AreEqual(3, AllProducts.Count(), "Expected 3 products");

        }
        

        // Testing non-query methods
         

        /// <summary>
        /// Deletes a product.
        /// </summary>
        [Test]
        public void DeleteAProduct()
         {             
            var objContext = mockEFContext.Object;
            var Products = objContext.Products.Count();
            Product ProductToRemove = mockEFContext.Object.Products.FirstOrDefault(x => x.ID == 2);


            //var mockContext = new Mock<IHypermartContext>();
            mockEFContext.Setup(m => m.Products).Returns(mockSet.Object);
            mockEFContext.Setup(m => m.Products.Find(ProductToRemove.ID)).Returns(ProductToRemove);
 
            //Act
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockEFContext.Object);
            CUT.DeleteProduct(ProductToRemove.ID);
            mockEFContext.Verify(m => m.Products.Remove(ProductToRemove), Times.Once());
            mockEFContext.Verify(m => m.SaveChanges(), Times.Once());
            


        }

        /// <summary>
        /// Determines whether this instance can delete a product.
        /// </summary>
         
        public void CanDeleteProduct()
        {
            //Arrange
            var _MockRepo = new MockRepository(MockBehavior.Strict);

            var productToDelete = new Product
            {
                ID = 100,
                Description = "Product 100",
                Files = null,
                Lead = "Lead Developer",
                Maintainers = new List<string> { "Maintainer 1", "Maintainer2" },
                Screenshots = null,
                SourceCode = "",
                Tagline = "tagline",
                Title = "title"

            };

           
            // Mocking a DB Set
            IEnumerable<Product > productList = new List<Product>();
            var productData = productList.AsQueryable();
            
            var _productSet = _MockRepo.Create<DbSet<Product>>();

            //_productSet.As<IDbSet<Product>>().Setup(x => x.Local).Returns(productData);
            //_productSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(productData.Provider);
           // _productSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(productData.Expression);
            //_productSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(productData.ElementType);
            //_productSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(productData.GetEnumerator());
           


            //var _MockHypermartContext =  _MockRepo.Create<IHypermartContext>();
            //_MockHypermartContext.Setup(m => m.Products).Returns(_productSet.Object);





            mockEFContext.Setup(x => x.Products.Find(productToDelete.ID)).Returns(productToDelete);
            mockEFContext.Setup(x => x.Products.Remove(productToDelete)).Returns(productToDelete);
            mockEFContext.Setup(x => x.SaveChanges()).Verifiable();
            //_MockHypermartContext.Setup(x => x.Products.Local).Returns(new ObservableCollection<Product>());




            //var _MockHypermartProductDB = _MockRepo.Create<IDbSet<Product>>();
            //_MockHypermartProductDB.As<IDbSet<Business>>().Setup(m => m.Provider).Returns(queryableTestData.Provider);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.Expression).Returns(queryableTestData.Expression);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.ElementType).Returns(queryableTestData.ElementType);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.GetEnumerator()).Returns(() => queryableTestData.GetEnumerator());
            //_MockHypermartProductDB.As<IDbSet<Product>>().Setup(m => m.Local).Returns(null);




            var _MockProductsList = _MockRepo.Create<IList<Product>>();








            var CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockEFContext.Object);


            //Act
            CUT.DeleteProduct(100);
            _MockRepo.VerifyAll();
        }



        [Test]
        public void CanAddNewProductMoq()
        {
            //Arrange

     
            var ProductToAdd = new Product() { ID = 4, Description = "d4", Files = null, Lead = "l4", Screenshots = null, Tagline = "tl4", Title = "title4" };
            mockEFContext.Setup(m => m.Products.Add(It.IsAny<Product>())).Returns(ProductToAdd);

            
            //Act
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockEFContext.Object );

            var res = CUT.PostProduct(ProductToAdd);
             

            mockEFContext.Verify(m => m.Products.Add(ProductToAdd), Times.Once());
            mockEFContext.Verify(m => m.SaveChanges(), Times.Once());
            

        }
 
 



       [Test]
        public void CanListAllFiles()
        {
            mockEFContext.Setup(c => c.Files).Returns(mockdbSetFiles.Object);
           var Files = mockEFContext.Object.Files; 
 
            Assert.AreEqual(3, Files.Count()); 
        }

 
        private class MockProducts : DbSet<Product>, IQueryable<Product>
        {
            protected List<Product> InMemoryList = new List<Product>();

            public override Product Find(params object[] keyValues)
            {
                var id = (int)keyValues.Single();
                return InMemoryList.SingleOrDefault(b => b.ID == id);
            } 
            public override Product Add(Product entity)
            {
                InMemoryList.Add(entity);
                return entity;
            }


            IEnumerator<Product> IEnumerable<Product>.GetEnumerator()
            {
                return InMemoryList.AsQueryable().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return InMemoryList.AsQueryable().GetEnumerator();
            }

            Type IQueryable.ElementType
            {
                get { return InMemoryList.AsQueryable().ElementType; }
            }

            System.Linq.Expressions.Expression IQueryable.Expression
            {
                get { return InMemoryList.AsQueryable().Expression; }
            }

            IQueryProvider IQueryable.Provider
            {
                get { return InMemoryList.AsQueryable().Provider; }
            }
        }


        private class ProdustList : MockDBSet<Product>
        {
            public override Product Find(params object[] keyValues)
            {
                var id = (int)keyValues.Single();
                return this.SingleOrDefault(b => b.ID == id);
            }
        }

        private class FilesList : MockDBSet<Open.GI.hypermart.Models.File>
        {
            public override Open.GI.hypermart.Models.File Find(params object[] keyValues)
            {
                var id = (int)keyValues.Single();
                return this.SingleOrDefault(b => b.ID == id);
            }
        }

        
        
        private class MockDBSet<T> : DbSet<T>, IQueryable<T> where T : class 
        {
            List<T> InMemoryList = new List<T>();
            
 
            public override T Add(T entity)
            {
                InMemoryList.Add(entity);
                return entity;
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return InMemoryList.AsQueryable().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return InMemoryList.AsQueryable().GetEnumerator();
            }

            Type IQueryable.ElementType
            {
                get { return InMemoryList.AsQueryable().ElementType; }
            }

            System.Linq.Expressions.Expression IQueryable.Expression
            {
                get { return InMemoryList.AsQueryable().Expression; }
            }

            IQueryProvider IQueryable.Provider
            {
                get { return InMemoryList.AsQueryable().Provider; }
            }
        }


        private class MockCntext : IHypermartContext
        {
            public IDbSet<Open.GI.hypermart.Models.File> Files { get; set; }
            
            public IDbSet<Platform> Platforms { get; set; }

            public IDbSet<Product> Products{get; set; }

            public IDbSet<Screenshot> Screenshots{ get; set; }

            public IDbSet<Rating> Ratings { get; set; }

            public IDbSet<RatingDetails> RatingDetails{get;set;}


            public void SaveChanges()
            {
               // throw new NotImplementedException();
            }

            public void Dispose()
            {
                // Do nothing
            }


         
        }

 



        private void  dataAdd(Product obj)
        {
            fakeProducts.Add(obj);
             

        }
    
    }
}
