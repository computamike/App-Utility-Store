using Open.GI.hypermart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Services
{
    /// <summary>
    /// Performs Searches.
    /// </summary>
    public class SearchService
    {
        private IHypermartContext _db;
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchService"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public SearchService(IHypermartContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Performs the search.
        /// </summary>
        /// <param name="SearchTerm">The search term.</param>
        /// <returns></returns>
        public IQueryable<Models.Product> PerformSearch(string SearchTerm)
        {
            var TitleMatches = _db.Products.Where(x => x.Title.ToUpperInvariant().Contains(SearchTerm.ToUpperInvariant().Trim()));
            var DescriptionMatches = _db.Products.Where(x => x.Description.ToUpperInvariant().Contains(SearchTerm.ToUpperInvariant().Trim()));
            var result = TitleMatches.Union(DescriptionMatches);
            
            return result;
        }
    }
}