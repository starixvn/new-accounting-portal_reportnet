WinLossByProduct = function () {
    var self = this;

    self.Tables = {
        btnGenerate: null,
        btnToday: null,
        btnYesterday: null,
        datepickerplugin: null,
        ddlSiteType: null,
        ddlParlayType: null
    };

    self.Params = {
        reportParams: {
            FromDate: null,
            ToDate: null,
            Site: 0,
            ParlayType: 0
        }
    };
};

WinLossByProduct.prototype.InitPage = function () {
    ddlParlayTypeItems = [{
            Id: "1_MixParlay",
            Value: 1,
            Text: "Mix Parlay",
            Checked: true,
            Enable: true
        },
        {
            Id: "2_BetBuilder",
            Value: 2,
            Text: "Bet Builder+",
            Checked: true,
            Enable: true,
        }
    ]
    $("#ddlParlayType").multiSelect(
        {
            minWidth: 140,
            maxHeight: 450,
            shadow: true,
            showHeader: true,
            isCheckAll: true,
            showFooter: false,
            showTooltip: true,
            checkAllText: 'All',
            isUsedCustomCheckAllText: true,
            customCheckAllText: 'All',
            listData: ddlParlayTypeItems
        });
    this.CachedDOMElements();
};

WinLossByProduct.prototype.CachedDOMElements = function () {
    this.Tables.btnGenerate = $("#btnGenerate");
    this.Tables.btnToday = $("#btnToday");
    this.Tables.btnYesterday = $("#btnYesterday");
    this.Tables.datepickerplugin = $("#datepickerplugin");
    this.Tables.fromDatepicker = $(".ui_nexdatepicker_fromdate");
    this.Tables.toDatepicker = $(".ui_nexdatepicker_todate");
    this.Tables.ddlSiteType = $("#ddlSiteType");
    this.Tables.ddlParlayType = $("#ddlParlayType");
    this.Tables.reportContainer = $('#advanceMixParlayReportContainer');
    this.Tables.report = $('#advanceMixParlayReport');
};

WinLossByProduct.prototype.BindingEvent = function () {
    var self = this;

    self.Tables.btnGenerate.unbind("click");
    self.Tables.btnGenerate.click(function () {
        self.GetReport(reportManager.Enum.DateType.SpecifyDay);
    });

    self.Tables.btnToday.unbind("click");
    self.Tables.btnToday.click(function () {
        self.Tables.fromDatepicker.datepicker("setDate", new Date(self.Tables.btnToday.val()));
        self.Tables.toDatepicker.datepicker("setDate", new Date(self.Tables.btnToday.val()));
        self.GetReport(reportManager.Enum.DateType.ToDay);
    });

    self.Tables.btnYesterday.unbind("click");
    self.Tables.btnYesterday.click(function () {
        self.Tables.fromDatepicker.datepicker("setDate", new Date(self.Tables.btnYesterday.val()));
        self.Tables.toDatepicker.datepicker("setDate", new Date(self.Tables.btnYesterday.val()));
        self.GetReport(reportManager.Enum.DateType.YesterDay);
    });
};

WinLossByProduct.prototype.GetReport = function (dateType) {
    var self = this;
    self.GetParams(dateType);
    if (self.ValidateParams() == true) {
        AccReport.disableInput();
        $.ajax({
        type: "POST",
        url: window.rootUrl + "/api/parlay/product",
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
    } else {
        AccReport.enableInput();
    }
    
};

WinLossByProduct.prototype.ValidateParams = function () {
        var isValid = true;
        var message = '';
        var notifyIds = [];

        if (isNaN(this.Params.reportParams.ParlayType)) {
            if (message == '') {
                message = "Please select at least one Parlay Type!";
            }
            notifyIds.push("ddlParlayType");
            isValid = false;
        }

        if (!isValid) {
            AccReport.popUp.removeHighLight();
            AccReport.popUp.show(message, notifyIds.join(","));
        } else {
            AccReport.popUp.removeHighLight();
        }

        return isValid;
    };

WinLossByProduct.prototype.GetParams = function (dateType) {
    switch (dateType) {
        case reportManager.Enum.DateType.SpecifyDay:
            var dateRange = this.Tables.datepickerplugin.nexDatePicker("getDateRange");
            this.Params.reportParams.FromDate = dateRange.FromDate;
            this.Params.reportParams.ToDate = dateRange.ToDate;
            break;
        case reportManager.Enum.DateType.ToDay:
            this.Params.reportParams.FromDate = this.Tables.btnToday.val();
            this.Params.reportParams.ToDate = this.Tables.btnToday.val();
            break;
        case reportManager.Enum.DateType.YesterDay:
            this.Params.reportParams.FromDate = this.Tables.btnYesterday.val();
            this.Params.reportParams.ToDate = this.Tables.btnYesterday.val();
            break;
    }

    this.Params.reportParams.Site = this.Tables.ddlSiteType.val();
    var parlayType = this.Tables.ddlParlayType.multiSelect("getCheckedItems");
    this.Params.reportParams.ParlayType = parlayType.length === 2 ? 0 : parseInt(parlayType.toString());
};

WinLossByProduct.prototype.RenderReport = function (report) {
    var self = this;

    reportManager.HideCombinationWinLossReport();
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
                    columns.push({ Name: "ColMemberBonus", Value: info.MemberBonus });
                    columns.push({ Name: "ColHouseBonus", Value: info.HouseBonus });
                    bodyData.push({
                        Columns: columns
                    });
                }

                info = sportGroup.Total;
                bodyData.push({
                    CssClass: "betTypeTotal",
                    Columns: [{ Name: "ColBetType", Value: info.Name, CssClass: "align-center" },
                    { Name: "ColWinLoss", Value: info.MemberWinLoss },
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
                    { Name: "ColMargin", Value: info.Margin },
                    { Name: "ColMemberBonus", Value: info.MemberBonus },
                    { Name: "ColHouseBonus", Value: info.HouseBonus }]
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
                    { Name: "ColMargin", Value: info.Margin },
                    { Name: "ColMemberBonus", Value: info.MemberBonus },
                    { Name: "ColHouseBonus", Value: info.HouseBonus }]
        });
    }

    var gridOptions = {
        customizeHeader: false,
        columns: [{ HeaderText: "", Width: 140, Name: "ColLiveType", HeaderAlign: "center", CellAlign: "center" },
                      { HeaderText: "Sport Type", Width: 140, Name: "ColSportType", HeaderAlign: "center", CellAlign: "right" },
                      { HeaderText: "Bet Type", Width: 120, Name: "ColBetType", HeaderAlign: "center", CellAlign: "left" },
                      { HeaderText: "Member Win Loss", Width: 120, Name: "ColWinLoss", HeaderAlign: "center", CellAlign: "right", FormatNumber: FormatConstant.TWO_DECIMAL_NUMBER, FormatPositive: '$' },
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
                text: 'Parlay Win Loss by Product  by ' + reportManager.GetSiteTypeName(this.Params.reportParams.Site) +
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
        gridWidth: 900,
        numberOfFrozenColumn: 0,
        showDistinctRow: false,
        showHighlightRow: false,
        noDataText: viewResources.EmptyReportMessage,
        marginRight: 20
    };

    self.Tables.report.removeClass("display-none").fanexGrid(gridOptions);
};

WinLossByProduct.prototype.HideReport = function () {
    this.Tables.reportContainer.addClass("display-none");
};

WinLossByProduct.prototype.ExportReport = function () {
    AccReport.utility.CallAjaxForDownload(this,
        window.rootUrl + "/api/parlay/excel-product",
        this.Params.reportParams,
        function (rs, fileName) {
            element = document.createElement('a');
            element.href = window.URL.createObjectURL(rs);
            element.download = fileName;
            element.style.display = 'none';
            document.body.appendChild(element);
            element.click();
            element.remove();
        },
        null,
        AccReport.enableInput());
};