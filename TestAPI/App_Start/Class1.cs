using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Open.GI.hypermart;
using System.Web.Optimization;
namespace TestAPI.App_Start
{
    [TestFixture]
    public class TestBundleConfiguration
    {

       
 

        [Test]
        public void TestThatBundleConfigurationCreatesTheCorrectBundles()
        {
            Mock<BundleCollection> mockBundleCollection = new Mock<BundleCollection>();
             

            BundleConfig.RegisterBundles(mockBundleCollection.Object);
        }
    }
}
