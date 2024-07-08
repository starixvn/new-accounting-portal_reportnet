AdvanceMixParlayReport = function () {
    var self = this;

    self.Params = {
        reportParams: {
            FromDate: null,
            ToDate: null,
            IsHistoryReport: null
        }
    };

    self.CachedData = {
        reportWidth: 955
    };
};

AdvanceMixParlayReport.prototype.InitPage = function () {
    this.CachedDOMElements();
};

AdvanceMixParlayReport.prototype.CachedDOMElements = function () {
    this.Tables.reportContainer = $('#advanceMixParlayReportContainer');
    this.Tables.report = $('#advanceMixParlayReport');
};

AdvanceMixParlayReport.prototype.GetReport = function (params) {
    var self = this;

    AccReport.disableInput();
    self.GetParams(params);
    $.ajax({
        type: "POST",
        url: window.rootUrl + "api/parlay/product",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(self.Params.reportParams),
        beforeSend: function () {
        },
        success: function (rs) {
            self.RenderReport(rs);
        },
        error: function (event, jqXHR, ajaxSettings) {
            self.RenderReport(null);
        },
        complete: function () {
            AccReport.enableInput();
        }
    });
};

AdvanceMixParlayReport.prototype.GetParams = function (params) {
    this.Params.reportParams.FromDate = params.FromDate;
    this.Params.reportParams.ToDate = params.ToDate;
    this.Params.reportParams.IsHistoryReport = false;
};

AdvanceMixParlayReport.prototype.RenderReport = function (report) {
    var self = this;
    
    self.Tables.reportContainer.removeClass("display-none");
    self.Tables.report.addClass("display-none");
    var bodyData = [];
    var footerData = [];
    if (report && report.WinLosses.length > 0) {
        var len = report.WinLosses.length;
        var currentRow = null;
        var info = null;
        for (var i = 0; i < len; i++) {
            var liveTypeGroup = report.WinLosses[i];
            var liveTypeGroupLength = liveTypeGroup.WinLosses.length;
            for (var j = 0; j < liveTypeGroupLength; j++) {
                var sportGroup = liveTypeGroup.WinLosses[j];
                var sportGroupLength = sportGroup.WinLosses.length;
                for (var k = 0; k < sportGroupLength; k++) {
                    info = sportGroup.WinLosses[k];
                    var columns = [];
                    if (j == 0 && k == 0) {
                        columns.push({
                            Name: "ColLiveType",
                            Value: liveTypeGroup.Name,
                            MergeRowNumber: liveTypeGroup.MergeRowNumber,
                            TdClass: "bold col" + liveTypeGroup.Name.replace(/\s+/g, '')
                        });
                    }

                    if (k == 0) {
                        columns.push({
                            Name: "ColSportType",
                            Value: sportGroup.Name,
                            MergeRowNumber: sportGroup.MergeRowNumber,
                            CssClass: "bold align-center"
                        });
                    }

                    columns.push({ Name: "ColBetType", Value: info.Name });
                    columns.push({ Name: "ColWinLoss", Value: info.MemberWinLoss });
                    columns.push({ Name: "ColPosition", Value: info.SMAWinLoss });
                    columns.push({ Name: "ColExtraPT", Value: info.ExtraPT });
                    columns.push({ Name: "ColComm", Value: info.Commission });
                    columns.push({ Name: "ColLineTotal", Value: info.RealIncome });
                    columns.push({ Name: "ColTurnover", Value: info.RealTurnover });
                    columns.push({ Name: "ColMargin", Value: info.Margin });
                    bodyData.push({
                        Columns: columns
                    });
                }

                info = sportGroup.Total;
                bodyData.push({
                    CssClass: "betTypeTotal",
                    Columns: [{ Name: "ColBetType", Value: info.Name, CssClass: "align-center" },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss, true },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin}]
                });
            }

            info = liveTypeGroup.Total;
            bodyData.push({
                CssClass: "subTotal",
                Columns: [{ Name: "ColSportType", Value: "" },
                    { Name: "ColBetType", Value: info.Name, CssClass: "align-center" },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin}]
            });
        }

        info = report.Total;
        footerData.push({
            Columns: [{ Name: "ColLiveType", Value: info.Name, MergeColNumber: 3 },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
                    { Name: "ColPosition", Value: info.SMAWinLoss },
                    { Name: "ColExtraPT", Value: info.ExtraPT },
                    { Name: "ColComm", Value: info.Commission },
                    { Name: "ColLineTotal", Value: info.RealIncome, TdClass: "colLineTotal" },
                    { Name: "ColTurnover", Value: info.RealTurnover },
                    { Name: "ColMargin", Value: info.Margin}]
        });
    }

    var gridOptions = {
        customizeHeader: false,
        columns: [{ HeaderText: "", Width: 140, Name: "ColLiveType", HeaderAlign: "center", CellAlign: "center" },
                      { HeaderText: "Sport Type", Width: 140, Name: "ColSportType", HeaderAlign: "center", CellAlign: "right" },
                      { HeaderText: "Bet Type", Width: 120, Name: "ColBetType", HeaderAlign: "center", CellAlign: "left" },
                      { HeaderText: "Member Win Loss", Width: 120, Name: "ColWinLoss", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "SMA Win Loss", Width: 100, Name: "ColPosition", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Extra PT", Width: 120, Name: "ColExtraPT", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Commission", Width: 100, Name: "ColComm", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Real Income", Width: 120, Name: "ColLineTotal", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Real Turnover", Width: 120, Name: "ColTurnover", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER },
                      { HeaderText: "Margin", Width: 100, Name: "ColMargin", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_PERCENT_NUMBER}],
        showTitle: true,
        showFooter: true,
        titleInfo:
            {
                text: 'Parlay Win Loss by Product - From ' + moment(this.Params.reportParams.FromDate).format(FormatConstant.MMM_DD_YYYY_STRING) +
                      ' To ' + moment(this.Params.reportParams.ToDate).format(FormatConstant.MMM_DD_YYYY_STRING),
                showExcelButton: true,
                exportExcelFunction: function () {
                    self.ExportReport();
                }
            },
        showTree: false,
        bodyRows: bodyData,
        footerRows: footerData,
        fullHeight: false,
        gridExpandHeight: 500,
        gridWidth: 900,
        numberOfFrozenColumn: 0,
        showDistinctRow: false,
        noDataText: viewResources.EmptyReportMessage,
        showHighlightRow: false
    };

    self.Tables.report.removeClass("display-none").fanexGrid(gridOptions);
    self.Tables.report.find(".free-cell-top-right").css("height", "30px");
};

AdvanceMixParlayReport.prototype.HideReport = function () {
    this.Tables.reportContainer.addClass("display-none");
};

AdvanceMixParlayReport.prototype.ExportReport = function () {
    AccReport.downloadFile(window.rootUrl + "WinLoss/DailyMixParlayWinLoss/ExportAdvanceMixParlayReport",
        this.Params.reportParams);
};