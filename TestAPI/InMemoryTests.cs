using NUnit.Framework;
using Open.GI.hypermart.Controllers;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TitaniumBunker.PhonySession;

namespace TestAPI
{
    [TestFixture]
    public class InMemoryTests
    {
        private  Stream GetResourceAsStrream(string streamName)
        {
            return  Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName);
        }

        [Test]
        public void Can_add_a_product_via_a_controller()
        {
            IHypermartContext x = new FakeHypermartContext();

            var controller = new ProductsController { db = x };

            var fakeHTTPSession = new  FonySession();
            fakeHTTPSession.AddFileUpload(new PhonyUploadFile("Screensjot.jpg", GetResourceAsStrream("TestAPI.img100.jpg"), "JPG"));
            controller.ControllerContext = fakeHTTPSession.BuildControllerContext(controller);
            controller.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());


            var res = controller.Create(new Product
            {
                ID = 1,
                Description = "NewProd",
                Files = null,
                Lead = "Lead USer",
                Maintainers = new List<String> { "m1", "m2", "m3" },
                Screenshots = new List<Screenshot>(),
                SourceCode = null,
                Tagline = "TagLine",
                Title = "Title"
            });


            Assert.AreEqual(1, controller.db.Products.Count());



        }


    }


}
