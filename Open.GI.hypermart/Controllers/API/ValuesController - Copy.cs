using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    /// Values Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ValuesControllerCopy : ApiController
    {   /// <summary>
        /// Gets all of the values
        /// </summary>
        /// <returns></returns>
        [Route("api/Values/GetByStatus")]
        public IEnumerable<string> GetByStatus(int Status)
        {
            return new string[] { "valueGBS1", "valueGBS2" };
        }



        /// <summary>
        /// Gets all of the values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}