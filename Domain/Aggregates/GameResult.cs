using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class GameResult
    {
        public int UserIntId { get; set; }
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        public int ScorePoints { get; set; }
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        public int Position { get; set; }
    }
}
