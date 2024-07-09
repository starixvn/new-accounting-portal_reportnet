CombinationWinLoss_HistoryReport = function () {
    var self = this;

    self.Tables = {
        btnGenerate: null,
        monthPicker: null,
        ddlReport: null,
        ddlSiteType_HistoryReport: null
    };

    self.Params = {
        reportParams: {
            FromDate: null,
            ToDate: null,
            Site: 0
        }
    };
};

CombinationWinLoss_HistoryReport.prototype.InitPage = function () {
    this.CachedDOMElements();
};

CombinationWinLoss_HistoryReport.prototype.CachedDOMElements = function () {
    this.Tables.ddlReport_HistoryReport = $("#ddlReport_HistoryReport");
    this.Tables.btnGenerate_HistoryReport = $("#btnGenerate_HistoryReport");
    this.Tables.monthPicker = $('#ddlMonth');
    this.Tables.ddlSiteType_HistoryReport = $('#ddlSiteType_HistoryReport');

    this.Tables.reportContainer = $('#combinationReportContainer_HistoryReport');
    this.Tables.report = $('#combinationReport_HistoryReport');
};

CombinationWinLoss_HistoryReport.prototype.BindingEvent = function () {
    var self = this;

    self.Tables.btnGenerate_HistoryReport.unbind("click");
    self.Tables.btnGenerate_HistoryReport.click(function () {
        self.GetReport();
    });
};

CombinationWinLoss_HistoryReport.prototype.GetReport = function () {
    var self = this;

    AccReport.disableInput();
    self.GetParams();
    $.ajax({
        type: "POST",
        url: window.rootUrl + "api/parlay/combination",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(self.Params.reportParams),
        beforeSend: function () {
        },
        success: function (rs) {
            AccReport.popUp.hide();
            self.RenderReport(rs);
        },
        error: function (jqXHR, event, ajaxSettings) {
            if (jqXHR.status && jqXHR.status == AccReportData.Constants.HtmlErrorCode.SystemError) {
                var r = jQuery.parseJSON(jqXHR.responseText);
                AccReport.popUp.show(r.message);
                self.RenderReport(null);
            }
        },
        complete: function () {
            AccReport.enableInput();
        }
    });
};

CombinationWinLoss_HistoryReport.prototype.GetParams = function (dateType) {
    this.Params.reportParams.FromDate = this.Tables.monthPicker.monPickr("getFromDate", FormatConstant.MMM_DD_YY_STRING_MONTH_PICKER);
    this.Params.reportParams.ToDate = this.Tables.monthPicker.monPickr("getToDate", FormatConstant.MMM_DD_YY_STRING_MONTH_PICKER);
    this.Params.reportParams.Site = this.Tables.ddlSiteType_HistoryReport.val();
};

CombinationWinLoss_HistoryReport.prototype.RenderReport = function (report) {
    var self = this;

    reportManager.HideAdvanceMixParlayReport_HistoryReport();
    self.Tables.reportContainer.removeClass("display-none");
    self.Tables.report.addClass("display-none");
    var bodyData = [];
    var footerData = [];
    if (report && report.WinLosses.length > 0) {
        var len = report.WinLosses.length;
        var currentRow = null;
        var info = null;
        for (var i = 0; i < len; i++) {
            var combinationGroup = report.WinLosses[i];
            var combinationGroupLength = combinationGroup.WinLosses.length;
            for (var j = 0; j < combinationGroupLength; j++) {
                var liveTypeGroup = combinationGroup.WinLosses[j];
                var liveTypeGroupLength = liveTypeGroup.WinLosses.length;
                for (var k = 0; k < liveTypeGroupLength; k++) {
                    info = liveTypeGroup.WinLosses[k];
                    var columns = [];
                    if (j == 0 && k == 0) {
                        columns.push({
                            Name: "ColCombination",
                            Value: combinationGroup.Name,
                            MergeRowNumber: combinationGroup.MergeRowNumber,
                            TdClass: "bold col" + combinationGroup.Name.replace(/\s+/g, '').replace(/&/g, 'And')
                        });
                    }

                    if (k == 0) {
                        columns.push({
                            Name: "ColType",
                            Value: liveTypeGroup.Name,
                            MergeRowNumber: liveTypeGroup.MergeRowNumber,
                            CssClass: "bold align-center"
                        });
                    }

                    columns.push({ Name: "ColBetType", Value: info.Name });
                    columns.push({ Name: "ColWinLoss", Value: info.MemberWinLoss });
                    columns.push({ Name: "ColMemberTurnover", Value: info.MemberTurnover });
                    columns.push({ Name: "ColBetCount", Value: info.BetCount });
                    columns.push({ Name: "ColPosition", Value: info.SMAWinLoss });
                    columns.push({ Name: "ColExtraPT", Value: info.ExtraPT });
                    columns.push({ Name: "ColComm", Value: info.Commission });
                    columns.push({ Name: "ColLineTotal", Value: info.RealIncome });
                    columns.push({ Name: "ColTurnover", Value: info.RealTurnover });
                    columns.push({ Name: "ColMargin", Value: info.Margin });
                    columns.push({ Name: "ColMemberBonus", Value: info.MemberBonus });
                    columns.push({ Name: "ColHouseBonus", Value: info.HouseBonus });
                    bodyData.push({
                        Columns: columns
                    });
                }

                info = liveTypeGroup.Total;
                bodyData.push({
                    CssClass: "betTypeTotal",
                    Columns: [{ Name: "ColBetType", Value: info.Name, CssClass: "align-center" },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
                    { Name: "ColMemberTurnover", Value: info.MemberTurnover },
                    { Name: "ColBetCount", Value: info.BetCount },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin },
                    { Name: "ColMemberBonus", Value: info.MemberBonus },
                    { Name: "ColHouseBonus", Value: info.HouseBonus }]
                });
            }

            info = combinationGroup.Total;
            bodyData.push({
                CssClass: "subTotal",
                Columns: [{ Name: "ColType", Value: "" },
                    { Name: "ColBetType", Value: info.Name, CssClass: "align-center" },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
                    { Name: "ColMemberTurnover", Value: info.MemberTurnover },
                    { Name: "ColBetCount", Value: info.BetCount },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin },
                    { Name: "ColMemberBonus", Value: info.MemberBonus },
                    { Name: "ColHouseBonus", Value: info.HouseBonus }]
            });
        }

        info = report.Total;
        footerData.push({
            Columns: [{ Name: "ColCombination", Value: info.Name, MergeColNumber: 3 },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
                    { Name: "ColMemberTurnover", Value: info.MemberTurnover },
                    { Name: "ColBetCount", Value: info.BetCount },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome, TdClass: "colLineTotal" },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin },
                    { Name: "ColMemberBonus", Value: info.MemberBonus },
                    { Name: "ColHouseBonus", Value: info.HouseBonus }]
        });
    }

    var gridOptions = {
        customizeHeader: true,
        groupHeaderRows: [{
            Columns: [{ HeaderText: "Combinations", Name: "ColCombination", MergeRowNumber: 2 },
                { HeaderText: "Type", Name: "ColType", MergeRowNumber: 2 },
                { HeaderText: "Bet Type", Name: "ColBetType", MergeRowNumber: 2 },
                { HeaderText: viewResources.Member, Name: "ColWinLoss", MergeColNumber: 3 },
                { HeaderText: viewResources.SMA_Win_Loss, Name: "ColPosition", MergeRowNumber: 2 },
                { HeaderText: "ExtraPT", Name: "ColExtraPT", MergeRowNumber: 2 },
                { HeaderText: "Commission", Name: "ColComm", MergeRowNumber: 2 },
                { HeaderText: viewResources.House, Name: "ColLineTotal", MergeColNumber: 3 },
                { HeaderText: "Member Bonus", Name: "ColMemberBonus", MergeRowNumber: 2 },
                { HeaderText: "House Bonus", Name: "ColHouseBonus", MergeRowNumber: 2 }]
        }],
        columns: [{ HeaderText: "Combinations", Width: 140, Name: "ColCombination", HeaderAlign: "center", CellAlign: "center" },
                      { HeaderText: "Type", Width: 140, Name: "ColType", HeaderAlign: "center", CellAlign: "right" },
                      { HeaderText: "Bet Type", Width: 120, Name: "ColBetType", HeaderAlign: "center", CellAlign: "left" },
                      { HeaderText: "Win Loss", Width: 120, Name: "ColWinLoss", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER, FormatPositive: '$' },
                      { HeaderText: "Turnover", Width: 120, Name: "ColMemberTurnover", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Bet Count", Width: 120, Name: "ColBetCount", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "SMA Win Loss", Width: 100, Name: "ColPosition", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Extra PT", Width: 120, Name: "ColExtraPT", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Commission", Width: 100, Name: "ColComm", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Real Income", Width: 120, Name: "ColLineTotal", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER, FormatPositive: '$' },
                      { HeaderText: "Real Turnover", Width: 120, Name: "ColTurnover", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Margin", Width: 100, Name: "ColMargin", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_PERCENT_NUMBER, FormatPositive: '$' },
                      { HeaderText: "Member Bonus", Width: 120, Name: "ColMemberBonus", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER, FormatPositive: '$' },
                      { HeaderText: "House Bonus", Width: 120, Name: "ColHouseBonus", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER, FormatPositive: '$' }],
        showTitle: true,
        showFooter: true,
        titleInfo:
            {
                text: 'Parlay Combination Win Loss by ' + reportManager.GetSiteTypeName(this.Params.reportParams.Site) +
                      ' - From ' + this.Params.reportParams.FromDate +
                      ' To ' + this.Params.reportParams.ToDate,
                showExcelButton: true,
                exportExcelFunction: function () {
                    self.ExportReport();
                }
            },
        showTree: false,
        bodyRows: bodyData,
        footerRows: footerData,
        gridExpandHeight: 230,
        fullWidth: true,
        numberOfFrozenColumn: 3,
        showDistinctRow: false,
        showHighlightRow: false,
        noDataText: viewResources.EmptyReportMessage,
        marginRight: 20
    };

    self.Tables.report.removeClass("display-none").fanexGrid(gridOptions);
};

CombinationWinLoss_HistoryReport.prototype.HideReport = function () {
    this.Tables.reportContainer.addClass("display-none");
};

CombinationWinLoss_HistoryReport.prototype.ExportReport = function () {
    AccReport.downloadFile(window.rootUrl + "WinLoss/DailyMixParlayWinLoss/ExportCombinationWinLossReport",
        this.Params.reportParams);
};