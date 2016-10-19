using Open.GI.hypermart.Controllers;
using Open.GI.hypermart.Helpers;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Open.GI.hypermart.Attributes
{
    /// <summary>
    /// Basic Authentication filter for WebAPI actions
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ActionFilterAttribute" />
    public class IdentityBasicAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                ReturnHttpError(actionContext, System.Net.HttpStatusCode.BadRequest);
                return;
            }

            string authToken = actionContext.Request.Headers.Authorization.Parameter;
            if (authToken == "Fail")
            {
                ReturnHttpError(actionContext, System.Net.HttpStatusCode.Unauthorized);
                return;
            }
            var f = HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(new WSUser() { Username = "Peter The Pirate" }), new string[] { });


        }


        /// <summary>
        /// Returns the HTTP error.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="StatusCode">The status code.</param>
        private void ReturnHttpError(HttpActionContext actionContext, System.Net.HttpStatusCode StatusCode)
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(StatusCode);
            var val = RenderView((int)StatusCode);
            actionContext.Response.Content = new StringContent(val);
            actionContext.Response.Content.Headers.ContentType.MediaType = "text/html";
            actionContext.Response.StatusCode = StatusCode;
        }
        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <param name="StatusCode">The status code.</param>
        /// <returns></returns>
        private string RenderView(int StatusCode)
        {
             var technicalContactEmail = System.Configuration.ConfigurationManager.AppSettings["TechnicalContactEmail"];
                 
            var ErrorList = new List<HttpErrorPage>();
            ErrorList.Add(new HttpErrorPage(){ErrorCode=400,ErrorTitle="Bad Request ",ErrorDescription="The server cannot process the request due to something that is perceived to be a client error.",TechnicalContact = technicalContactEmail});
            ErrorList.Add(new HttpErrorPage(){ErrorCode=401,ErrorTitle="Unauthorized",ErrorDescription="The requested resource requires authentication.",TechnicalContact = technicalContactEmail});
            ErrorList.Add(new HttpErrorPage(){ErrorCode=500,ErrorTitle="Webservice currently unavailable ",ErrorDescription="An unexpected condition was encountered.Our service team has been dispatched to bring it back online.",TechnicalContact = technicalContactEmail});
                         
            var response = HttpContext.Current.Response;
            
            var z = ErrorList.Where(x => x.ErrorCode == StatusCode).FirstOrDefault();

            if (z == null)
            {
                z = new HttpErrorPage()
                {
                    ErrorCode = StatusCode,
                    ErrorTitle = "Unknown Error",
                    ErrorDescription = "An unknown error has occured.",
                    TechnicalContact = technicalContactEmail
                };
            }


            // Create an arbitrary controller instance
            var controller = ViewRenderer.CreateController<FakeController>();
            
            string html = ViewRenderer.RenderView(
                                        string.Format("~/views/shared/HttpErrorPages/Error.cshtml", StatusCode.ToString()),
                                        z,
                                        controller.ControllerContext);
            return html;
        }

    }

    //internal class ErrorViewModel
    //{
    //}
}
