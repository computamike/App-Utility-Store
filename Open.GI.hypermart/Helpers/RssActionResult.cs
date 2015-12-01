using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
namespace Open.GI.hypermart.Helpers
{
    public class AtomActionResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/atom+xml";

            Atom10FeedFormatter AtomFormatter = new Atom10FeedFormatter(Feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                AtomFormatter.WriteTo(writer);
            }
        }
    }
    public class RssActionResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }
    }
}