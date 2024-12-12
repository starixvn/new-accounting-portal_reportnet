using System.Collections.Generic;

namespace BetConstructPerformance.Models
{
    public class SportModel
    {
        public IEnumerable<SportItemModel> Items { get; set; }

        public SportItemModel Total { get; set; }
    }
}
