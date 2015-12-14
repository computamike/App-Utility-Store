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


            MockDbContext = new MockCntext();

            var p = new ProdustList();

            p.Add(new Product() { ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" });
            p.Add(new Product() { ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" });
            p.Add(new Product() { ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" });

            
            
            MockDbContext.Products = p;  
        }

        [Test]
        public void CanListAllApps()
        {
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext);

            var ProductToUpdate = CUT.GetProducts(1);
            var AllProducts = CUT.GetAllProducts();
            
            Assert.AreEqual(3, AllProducts.Count(), "Expected 3 products");

        }
        
        [Test]
        public void CanAddNewProduct()
        {
            
            var ProductToAdd = new Product() { ID = 4, Description = "d4", Files = null, Lead = "l4", Screenshots = null, Tagline = "tl4", Title = "title4" };
            var ExpectedProductDTO = new Open.GI.hypermart.DataTransformationObjects.ProductDTO(ProductToAdd); 
            

            var mockProspect = new Mock<DbSet<Product>>() ;
            mockProspect.Object.Add(ProductToAdd);

            var MockDbContext = EntityFrameworkMockHelper.GetMockContext<HypermartContext>();
            MockDbContext.Object.Products.Add(new Product() { ID = 1, Description = "d1", Files = null, Lead = "l1", Screenshots = null, Tagline = "tl1", Title = "title1" });
            MockDbContext.Object.Products.Add(new Product() { ID = 2, Description = "d2", Files = null, Lead = "l2", Screenshots = null, Tagline = "tl2", Title = "title2" });
            MockDbContext.Object.Products.Add(new Product() { ID = 3, Description = "d3", Files = null, Lead = "l3", Screenshots = null, Tagline = "tl3", Title = "title3" });

            MockDbContext.Setup(m => m.Set<Product>()).Returns(mockProspect.Object );
            MockDbContext.Setup(m=> m.Products.Add(It.IsAny<Product>())).Returns(ProductToAdd);
             
            StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext.Object);
           
            var res = CUT.PostProduct(ProductToAdd);
            Assert.AreEqual(ExpectedProductDTO.ID, res.ID, "Expected product returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Description, res.Description, "Expected product returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Lead, res.Lead, "Expected prdouct returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Tagline, res.Tagline, "Expected product returned from Add Product call does not match expected product");
            Assert.AreEqual(ExpectedProductDTO.Title, res.Title, "Expected product returned from Add Product call does not match expected product");

        }

       [Test]
        public void CanAddNewProductFile()
        {
           StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController(MockDbContext);
           var ProductToUpdate = CUT.GetProducts(1);
           var OSC1 = new File
           {
               StorageType = storageType.RemoteShare,
               FileName = "OpenSuiteClient.msi",
               Link = @"\\bsdrel\thearchives\OpenSuiteClient\5.1.0\Cut03\OpenSuiteClient.msi",
               Platforms = new List<Platform> { platforms.Where(f => f.Platform1 == "Windows").First(), platforms.Where(f => f.Platform1 == "Linux").First() }
           };


          // CUT.AddFile(ProductToUpdate.ID, OSC1);
          // CUT.GetFiles(ProductToUpdate.ID);
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
            public DbSet<File> Files { get; set; }
            
            public DbSet<Platform> Platforms { get; set; }

            public DbSet<Product> Products{get; set; }

            public DbSet<Screenshot> Screenshots{ get; set; }
           

            public void SaveChanges()
            {
                throw new NotImplementedException();
            }
        }

 


        private void  dataAdd(Product obj)
        {
            fakeProducts.Add(obj);
             

        }
    
    }
}
