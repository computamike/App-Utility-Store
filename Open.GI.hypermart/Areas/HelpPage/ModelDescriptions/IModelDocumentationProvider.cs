using System;
using System.Reflection;

namespace Open.GI.hypermart.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        string GetDocumentation(Type type);
    }
}