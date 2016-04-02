using Open.GI.hypermart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Open.GI.hypermart.Controllers;

namespace Open.GI.hypermart
{
    /// <summary>
    /// Register system dependencies
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void RegisterDependencies(IUnityContainer container)
        {
           
            container.RegisterType<IHypermartContext, HypermartContext>();
            
        }
    }
}
