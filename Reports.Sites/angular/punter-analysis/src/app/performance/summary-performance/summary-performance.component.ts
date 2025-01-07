import { Component, Input } from '@angular/core';
import * as _ from 'lodash';
import { ChartDirective } from 'nap-components/chart';
import { DateHelper } from 'nap-components/date';
import { NumberFormatHelper, NumberFormatType } from 'nap-components/number-format';
import { FilterModelWrapper, PeriodType } from '../../filter/filter.model.main';
import { Performance, PerformanceType } from '../performance.model';
import { PerformanceService } from '../performance.service';
import { SummaryPerformanceService } from './summary-performance.service';
import { PerformanceStatistic } from './summary-performance.model';
import { RomanFormatter } from '../common/roman-formatter';

function rgba(colour: Array<number>, alpha: number): string {
    return 'rgba(' + colour.concat(alpha).join(',') + ')';
}

@Component({
    selector: 'nap-summary-performance',
    templateUrl: './summary-performance.component.html'
})
export class SummaryPerformanceComponent implements Performance {
    statistic: PerformanceStatistic;
    hasData = false;
    monthLabels: string[];
    marginDatasets: any[];
    marginOptions: any;
    betCountDatasets: any[];
    betCountOptions: any;
    winLossTurnoverDatasets: any[];
    winLossTurnoverOptions: any;
    productLabels: string[];
    productData: number[];
    productOptions: any;
    private winLossRomanFormatter: RomanFormatter;
    private turnoverRomanFormatter: RomanFormatter;

    constructor(
        private service: SummaryPerformanceService,
        private performanceService: PerformanceService
    ) {
        this.marginOptions = {
            title: {
                display: true,
                text: 'MARGIN (%) BY MONTH'
            },
            legend: {
                display: false
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem) {
                        return NumberFormatHelper.format(tooltipItem.yLabel, NumberFormatType.Decimal);
                    }
                }
            },
            scales: {
                xAxes: [{
                    barPercentage: 0.5
                }],
                yAxes: [{
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 0
                    }
                }]
            }
        };
        this.betCountOptions = {
            title: {
                display: true,
                text: 'BET COUNT BY MONTH'
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        const value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                        return NumberFormatHelper.format(value, NumberFormatType.Integer);
                    }
                }
            }
        };
        this.winLossTurnoverOptions = {
            title: {
                display: true,
                text: 'WINLOSS/TURNOVER'
            },
            tooltips: {
                mode: 'label',
                callbacks: {
                    label: function (tooltipItem) {
                        return NumberFormatHelper.format(tooltipItem.yLabel, NumberFormatType.Decimal);
                    }
                }
            },
            scales: {
                xAxes: [{
                    barPercentage: 0.5,
                    gridLines: {
                        display: false
                    }
                }],
                yAxes: [{
                    type: 'linear',
                    position: 'left',
                    id: 'y-axis-1',
                    gridLines: {
                        display: false
                    },
                    afterBuildTicks: (scale) => {
                        this.turnoverRomanFormatter = new RomanFormatter(scale.ticks);
                    },
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 0,
                        fontColor: rgba(ChartDirective.defaultColors[1], 1),
                        callback: (value) => {
                            return this.turnoverRomanFormatter.format(value);
                        }
                    },
                    scaleLabel: {
                        display: true,
                        labelString: 'TURNOVER',
                        fontColor: rgba(ChartDirective.defaultColors[1], 1)
                    }
                }, {
                    type: 'linear',
                    position: 'right',
                    id: 'y-axis-2',
                    gridLines: {
                        display: false
                    },
                    afterBuildTicks: (scale) => {
                        this.winLossRomanFormatter = new RomanFormatter(scale.ticks);
                    },
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 0,
                        fontColor: rgba(ChartDirective.defaultColors[0], 1),
                        callback: (value) => {
                            return this.winLossRomanFormatter.format(value);
                        }
                    },
                    scaleLabel: {
                        display: true,
                        labelString: 'WINLOSS',
                        fontColor: rgba(ChartDirective.defaultColors[0], 1)
                    }
                }]
            }, elements: {
                line: {
                    fill: false
                }
            }
        };
        this.productOptions = {
            title: {
                display: true,
                text: 'TURNOVER BY PRODUCT (Click on Sportsbook to view details)'
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        const value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                        return NumberFormatHelper.format(value, NumberFormatType.Decimal);
                    }
                }
            }
        };
    }

    get title(): string {
        if (this.performanceService.activePerformance === this) {
            const filterModel = this.performanceService.filterModel;
            const userName = filterModel.performance.userName;
            const dateString = FilterModelWrapper.getDateFromToString(PeriodType.MonthRange, filterModel.fromDate, filterModel.toDate);
            return `Punter ${userName}'s Performance ${dateString}`;
        } else {
            return 'Main';
        }
    }

    query() {
        this.statistic = this.performanceService.summaryPerformanceStatistic;
        this.hasData = this.statistic.MonthStatistics.length !== 0;
        const monthStatistics = this.statistic.MonthStatistics;

        // month labels
        this.monthLabels = monthStatistics.map(item => {
            const dateStruct = DateHelper.convertStringToDateStruct(item.Date);
            return `${dateStruct.month}/${dateStruct.year}`;
        });

        // margin by month
        this.marginDatasets = [{
            label: 'Margin',
            data: monthStatistics.map(item => _.round(item.WinLoss * 100 / item.Turnover, 2))
        }];

        // bet count by month
        this.betCountDatasets = [{
            data: monthStatistics.map(item => item.BetCount)
        }];

        // win loss / turnover by month
        this.winLossTurnoverDatasets = [{
            type: 'line',
            label: 'Win loss',
            data: monthStatistics.map(item => item.WinLoss),
            fill: false,
            yAxisID: 'y-axis-2'
        }, {
            type: 'bar',
            label: 'Turnover',
            data: monthStatistics.map(item => item.Turnover),
            fill: false,
            yAxisID: 'y-axis-1'
        }];

        this.generateProductTurnover();
    }

    chartClicked(e: any): void {
        if (e.active.length > 0) {
            const chartElement = e.active[0];
            const activeIndex = chartElement._index;
            const productTurnoverStatistic = this.statistic.ProductTurnoverStatistics[activeIndex];
            if (productTurnoverStatistic.ProductName === 'Sportsbook') {
                this.performanceService.generate(PerformanceType.Product);
            }
        }
    }

    private generateProductTurnover() {
        const productsTurnoverStatistics = this.statistic.ProductTurnoverStatistics;
        this.productLabels = productsTurnoverStatistics.map(item => item.ProductName);
        this.productData = productsTurnoverStatistics.map(item => item.Turnover);
    }
}
