using System.Collections.Generic;
using System.Linq;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Models;
using Nap_Reports.HouseWinLoss.Soccer.BaseService;

namespace Nap_Reports.HouseWinLoss.Soccer.SumBetType
{
    public interface SumBetTypeGridOptionsBuilder
    {
        GridOptions Build(SumBetTypeWinLossModel winLoss, string title);
    }

    public class SumBetTypeGridOptionsBuilderImp : BaseBetTypeGridOptionsBuilder, SumBetTypeGridOptionsBuilder
    {
        public SumBetTypeGridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
           : base(gridDataRowBuilder)
        {
        }

        public GridOptions Build(SumBetTypeWinLossModel winLoss, string title)
        {
            return new GridOptions
            {
                CustomizeHeader = true,
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = title.ToUpperInvariant(),
                BodyRows = BuildSumBetTypeBodyRows(winLoss.SiteWinLossSummaries),
                FooterRows = BuildFooter(winLoss),
                ShowFooter = true,
                ShowTree = true,
                ShowHighlightRow = false,
                ShowDistinctRow = false
            };
        }

        private List<GridDataRow> BuildSumBetTypeBodyRows(IEnumerable<SiteWinLossSummary> winLoss)
        {
            return winLoss
                .Select(item => BuildBetTypeDataRow(item, HAlignment.Left))
                .ToList();
        }
    }
}