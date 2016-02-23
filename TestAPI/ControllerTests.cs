using Moq;
using NUnit.Framework;
using Open.GI.hypermart.Controllers;
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
// Looking at the possile methods of testing a Controller / DB Context there seems to be three mechanism for doing this.
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
    [TestFixture]
    public class InMemoryTests
    {
        private System.IO.Stream GetResourceAsStrream(string streamName)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName);
        }

        [Test]
        public void Can_add_a_product_via_a_controller()
        {
            IHypermartContext x = new FakeHypermartContext();

            var controller = new ProductsController { db = x };

            var fakeHTTPSession = new TitaniumBunker.PhonySession.FonySession();
            fakeHTTPSession.AddFileUpload(new PhonyUploadFile("Screensjot.jpg", GetResourceAsStrream("TestAPI.img100.jpg"), "JPG"));
            controller.ControllerContext = fakeHTTPSession.BuildControllerContext(controller);
            controller.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());


            var res = controller.Create(new Product 
                {  
                    ID = 1,
                    Description ="NewProd",
                    Files = null,Lead ="Lead USer" ,Maintainers=new List<String>{"m1","m2","m3"} ,
                    Screenshots = new List<Screenshot>(),SourceCode = null,Tagline="TagLine", Title = "Title"
                });
            

            Assert.AreEqual(1,controller.db.Products.Count());
           

    
        }


    }


    [TestFixture]
    public class MOQTests
    {
        [Test]
        public void Can_Add_A_Product_Via_A_Controller()
        {
            var mockEFContext = new Mock<IHypermartContext>();
            var Products = new List<Product> 
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());

            mockEFContext.Setup(x => x.Products).Returns(mockSet.Object);
            
            var prodcontroller = new ProductsController() {db = mockEFContext.Object };

            var fakeHTTPSession = new TitaniumBunker.PhonySession.FonySession();
            fakeHTTPSession.AddFileUpload(new PhonyUploadFile("Screensjot.jpg", GetResourceAsStrream("TestAPI.img100.jpg"), "JPG"));
            prodcontroller.ControllerContext = fakeHTTPSession.BuildControllerContext(prodcontroller);
            prodcontroller.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());


            var res = prodcontroller.Create(new Product { ID = 2, Description = "foobar" });
        
        }

        private System.IO.Stream    GetResourceAsStrream(string streamName)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName );
        }
    }

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

             fakeProducts = new List<Product>
            {
                new Product{ ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" },
                new Product{ ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" },
                new Product{ ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" }
            };



            //var p = new ProdustList();

            //p.Add(new Product() { ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" });
            //p.Add(new Product() { ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" });
            //p.Add(new Product() { ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" });
            //MockDbContext.Products = p;  
            //var ProductFiles = new FilesList();

            //var ProductForFile=  MockDbContext.Products.Find(1);
            //ProductFiles.Add(new File { FileName = "Foo.exe", ID = 1, ProductID = 1, Product = ProductForFile });

            //MockDbContext.Files = ProductFiles;  
           
        }

        
        public void CanListAllApps()
        {
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext);

            var ProductToUpdate = CUT.GetProducts(1);
            var AllProducts = CUT.GetAllProducts();
            
            Assert.AreEqual(3, AllProducts.Count(), "Expected 3 products");

        }
        

        // Testing non-query methods


        /// <summary>
        /// Deletes a product.
        /// </summary>
        
        public void DeleteAProduct()
        {
            var mockSet = new Mock<DbSet<Product>>();
            var data = fakeProducts.AsQueryable();
            mockSet.Object.AddRange(fakeProducts);


            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            Product ProductToRemove = fakeProducts[2];

            var mockContext = new Mock<IHypermartContext>();
            mockContext.Setup(m => m.Products ).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products.Find(ProductToRemove.ID)).Returns(ProductToRemove);

            mockContext.Setup(m => m.Products.Remove(ProductToRemove)).Returns(mockSet.Object.Remove(ProductToRemove));


            var objContext = mockContext.Object;
            var Products = objContext.Products.Count();




            //Act
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockContext.Object );
            CUT.DeleteProduct(ProductToRemove.ID);


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
           


            var _MockHypermartContext =  _MockRepo.Create<IHypermartContext>();
            //_MockHypermartContext.Setup(m => m.Products).Returns(_productSet.Object);
            
          
            
            
            
            _MockHypermartContext.Setup(x => x.Products.Find(productToDelete.ID)).Returns(productToDelete);
            _MockHypermartContext.Setup(x => x.Products.Remove(productToDelete)).Returns(productToDelete);
            _MockHypermartContext.Setup(x => x.SaveChanges()).Verifiable();
            //_MockHypermartContext.Setup(x => x.Products.Local).Returns(new ObservableCollection<Product>());




            //var _MockHypermartProductDB = _MockRepo.Create<IDbSet<Product>>();
            //_MockHypermartProductDB.As<IDbSet<Business>>().Setup(m => m.Provider).Returns(queryableTestData.Provider);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.Expression).Returns(queryableTestData.Expression);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.ElementType).Returns(queryableTestData.ElementType);
            //mockSet.As<IDbSet<Business>>().Setup(m => m.GetEnumerator()).Returns(() => queryableTestData.GetEnumerator());
            //_MockHypermartProductDB.As<IDbSet<Product>>().Setup(m => m.Local).Returns(null);




            var _MockProductsList = _MockRepo.Create<IList<Product>>();

            

   

            


            var CUT = new  Open.GI.hypermart.Controllers.StoreContentController(_MockHypermartContext.Object );


            //Act
            CUT.DeleteProduct(100);
            _MockRepo.VerifyAll();
        }



        [Test]
        public void CanAddNewProductMoq()
        {
            //Arrange
            var mockEFContext = new Mock<IHypermartContext>();
            var Products = new List<Product> 
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());

            mockEFContext.Setup(x => x.Products).Returns(mockSet.Object);

            var ProductToAdd = new Product() { ID = 4, Description = "d4", Files = null, Lead = "l4", Screenshots = null, Tagline = "tl4", Title = "title4" };
            
            
            //Act
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(mockEFContext.Object );

            var res = CUT.PostProduct(ProductToAdd);
            var AllProducts = CUT.GetAllProducts();



        }

  
       
        public void CanAddNewProductFile_old()
        {
           //Arrange
           StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext);
           var ProductToUpdate = CUT.GetProducts(1);
           var OSC1 = new Open.GI.hypermart.Models.File
           {
               ProductID = ProductToUpdate.ID,
               StorageType = storageType.RemoteShare,
               FileName = "OpenSuiteClient.msi",
               Link = @"\\bsdrel\thearchives\OpenSuiteClient\5.1.0\Cut03\OpenSuiteClient.msi",
               Platforms = new List<Platform> { platforms.Where(f => f.Platform1 == "Windows").First(), platforms.Where(f => f.Platform1 == "Linux").First() }
           };

           //Act
           CUT.AddFile(ProductToUpdate.ID, OSC1);
         
           //Assert
           var x = CUT.GetFiles(ProductToUpdate.ID);
           Assert.AreEqual(2, x.Count, "There should be 2 files in the store");
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
