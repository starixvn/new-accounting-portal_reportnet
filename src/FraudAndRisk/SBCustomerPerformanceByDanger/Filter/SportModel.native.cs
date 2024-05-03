using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCustomerPerformanceByDanger.Filter
{
   public class SportModel
    {
        public int SportType { get; set; }

        public string SportName { get; set; }

        public List<BettypeGroupModel> BettypeGroups { get; set; }
    }
}
