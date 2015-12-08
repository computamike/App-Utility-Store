using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
namespace Open.GI.hypermart.Helpers
{
    /// <summary>
    /// Creates an ATOM Result
    /// </summary>
    /// <seealso cref="System.Web.Mvc.ActionResult" />
    public class AtomActionResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the feed.
        /// </summary>
        /// <value>
        /// The feed.
        /// </value>
        public SyndicationFeed Feed { get; set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
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
    /// <summary>
    /// RSS Action Results
    /// </summary>
    /// <seealso cref="System.Web.Mvc.ActionResult" />
    public class RssActionResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the feed.
        /// </summary>
        /// <value>
        /// The feed.
        /// </value>
        public SyndicationFeed Feed { get; set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
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