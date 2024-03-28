using System;
using Fanex.Data;
using Fanex.Data.Repository;

namespace Nap_Reports.HouseWinLoss.Common.Criteria
{
    public abstract class WinLossCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public bool NormalMode { get; set; } = true;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override bool IsValid() => true;
    }
}