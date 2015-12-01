using Open.GI.hypermart.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    public class NugetController : Controller
    {
        // GET: Nuget
        public ActionResult Index()
        {
            return View();
        }
        // GET: Nuget
        public AtomActionResult  Packages()
        {

           // var htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
            var CurrentController ="Nuget"; //htmlHelper.ViewContext.RouteData.Values["controller"];
            var CurrentAction = "Packaged";//htmlHelper.ViewContext.RouteData.Values["action"];

            var myFeedInstance = new SyndicationFeed();





            var FeedAddress = string.Format("{0}://{1}{2}/{3}/{4}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), CurrentController, CurrentAction);
            

            myFeedInstance.Title =  new TextSyndicationContent("Packages");
            myFeedInstance.Id = FeedAddress;
            myFeedInstance.Generator = "Mike Hingley - " + Request.Url.Scheme ;
            myFeedInstance.BaseUri = new Uri("http://localhost:44167/nuget/");
            myFeedInstance.Links.Add(new SyndicationLink() { Title = "Packages", Uri = new System.Uri("http://Packages"), RelationshipType = "self" });



            SyndicationItem item1 = new SyndicationItem();
            item1.Id = "http://localhost:44167/nuget/Packages(Id='NUnitTestAdapter',Version='2.0.0')";
            item1.Title = new TextSyndicationContent ("NUnitTestAdapter");
            item1.Summary = new TextSyndicationContent("");
            item1.LastUpdatedTime = DateTime.Now;
            item1.Authors.Add(new SyndicationPerson() { Name = "NunitSoftware" });
            
            item1.Links.Add(new SyndicationLink() { RelationshipType = "edit-media", Uri = new Uri("http://nuget.org/Packages(Id='NUnitTestAdapter',Version='2.0.0')/$value"), Title = "Package" });
            item1.Links.Add(new SyndicationLink() { RelationshipType = "edit", Uri =  new Uri("http://nuget.org/Packages(Id='NUnitTestAdapter',Version='2.0.0')"), Title = "Package" });

            item1.Categories.Add(new SyndicationCategory() { Scheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme", Name = "NuGet.Server.DataServices.Package" });
            
            
                 


            List<SyndicationItem> items = new List<SyndicationItem>();
            items.Add(item1);
            myFeedInstance.Items = items;
            Response.ContentType = "application/atom+xml";
            return new AtomActionResult() { Feed = myFeedInstance };
 
        }



    }
}