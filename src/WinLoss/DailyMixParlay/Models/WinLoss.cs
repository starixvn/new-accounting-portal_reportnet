using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMixParlay.Models
{
    public class WinLoss
    {
        public List<ParentGroupItem> WinLosses { get; set; }

        public WinLossItem Total { get;set; }
    }
}
