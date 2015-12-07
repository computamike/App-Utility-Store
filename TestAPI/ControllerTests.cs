using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Open.GI.hypermart;
namespace TestAPI
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void CanListAllApps()
        {
            Open.GI.hypermart.Controllers.StoreContentController CUT = new Open.GI.hypermart.Controllers.StoreContentController();
            CUT.GetAllProducts();
            //RestResponse response = client.Execute(request);
           // var content = response.Content; 
        }
    }
}
