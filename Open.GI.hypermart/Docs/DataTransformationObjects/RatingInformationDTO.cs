using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.DataTransformationObjects
{
    /// <summary>
    /// Rating information DTO
    /// </summary>
    public class RatingInformationDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingInformationDTO"/> class.
        /// </summary>
        public RatingInformationDTO()
        {
            Ratings = new List<RatingDTO>();
        }
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }
        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public int FileID { get; set; }
        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        public List<RatingDTO> Ratings { get; set; }
        /// <summary>
        /// Gets or sets the comment title.
        /// </summary>
        /// <value>
        /// The comment title.
        /// </value>
        public string CommentTitle { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }


    }

}
