using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.DataTransformationObjects
{
    /// <summary>
    /// Rating detail DTO
    /// </summary>
    public class RatingDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingDTO"/> class.
        /// </summary>
        /// <param name="RatedArea">The rated area.</param>
        /// <param name="score">The score.</param>
        /// <param name="outof">The outof.</param>
        public RatingDTO( string RatedArea, int score, int outof)
        {
            this.RatedArea = RatedArea;
            this.Score = score;
            this.OutOf = outof;

        }
        /// <summary>
        /// Gets or sets the rated area.
        /// </summary>
        /// <value>
        /// The rated area.
        /// </value>
        public string RatedArea { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }
        /// <summary>
        /// Gets or sets the out of.
        /// </summary>
        /// <value>
        /// The out of.
        /// </value>
        public int OutOf { get; set; }
    }
}

