using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class RatingSections
    {
        public RatingSections( string RatedArea, int score, int outof)
        {
            this.RatedArea = RatedArea;
            this.Score = score;
            this.OutOf = outof;

        }
        public string RatedArea { get; set; }
        public int Score { get; set; }
        public int OutOf { get; set; }
    }
}
