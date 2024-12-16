import { Component } from '@angular/core';
import { NumberFormatHelper, NumberFormatType } from 'nap-components/number-format';
import { FilterModelWrapper, PeriodType } from '../../filter/filter.model.main';
import { Performance } from '../performance.model';
import { PerformanceService } from '../performance.service';
import { ProductPerformanceService } from './product-performance.service';
import { SportsbookStatistic } from './product-performance.model';
import { RomanFormatter } from '../common/roman-formatter';
import { groupName, displayLimit } from '../common/config';

@Component({
    selector: 'nap-product-performance',
    templateUrl: './product-performance.component.html'
})
export class ProductPerformanceComponent implements Performance {
    statistic: SportsbookStatistic;
    betCountBySportLabels: string[];
    betCountBySportData: number[];
    betCountBySportOptions: any;
    eventTypeLabels: string[];
    betCountByEventTypeData: number[];
    betCountByEventTypeOptions: any;
    winLossLabels: string[];
    winLossDatasets: any[];
    winLossOptions: any;
    turnoverLabels: string[];
    turnoverData: number[];
    turnoverOptions: any;
    private winLossRomanFormatter: RomanFormatter;

    constructor(
        private service: ProductPerformanceService,
        private performanceService: PerformanceService
    ) {
        this.betCountBySportOptions = {
            title: {
                display: true,
                text: 'BET COUNT BY SPORT'
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
        this.betCountByEventTypeOptions = {
            title: {
                display: true,
                text: 'BET COUNT BY EVENT TYPE'
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
        this.winLossOptions = {
            title: {
                display: true,
                text: 'WINLOSS BY SPORT'
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
                    afterBuildTicks: (scale) => {
                        this.winLossRomanFormatter = new RomanFormatter(scale.ticks);
                    },
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 0,
                        callback: (value) => {
                            return this.winLossRomanFormatter.format(value);
                        }
                    }
                }]
            }
        };
        this.turnoverOptions = {
            title: {
                display: true,
                text: 'TURNOVER BY SPORT'
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
        const filterModel = this.performanceService.filterModel;
        const userName = filterModel.performance.userName;
        const dateString = FilterModelWrapper.getDateFromToString(PeriodType.MonthRange, filterModel.fromDate, filterModel.toDate);
        return `Punter ${userName}'s Performance ${dateString} - Detail from Sportsbook`;
    }

    query() {
        this.service.query().subscribe(statistic => {
            this.statistic = statistic;
            this.generateBetCountBySports();
            this.generateBetCountByEventType();
            this.generateWinLossBySports();
            this.generateWinLossBySports();
            this.generateTurnoverBySports();
        });
    }

    private generateBetCountByEventType() {
        const summaryPerformanceStatistic = this.performanceService.summaryPerformanceStatistic;
        this.eventTypeLabels = summaryPerformanceStatistic.EventStatistics.map(item => item.EventType);
        this.betCountByEventTypeData = summaryPerformanceStatistic.EventStatistics.map(item => item.BetCount);
    }

    private generateBetCountBySports() {
        const sportStatistics = this.statistic.SportStatistics;
        const orderList = sportStatistics.sort((a, b) => {
            if (a.BetCount > b.BetCount) {
                return -1;
            } else if (a.BetCount === b.BetCount) {
                return 0;
            }
            return 1;
        });
        const trimList = orderList.slice(0, displayLimit);
        this.betCountBySportLabels = trimList.map(item => item.SportName);
        this.betCountBySportData = trimList.map(item => item.BetCount);
        if (orderList.length > displayLimit) {
            this.betCountBySportLabels.push(groupName);
            let betCount = 0;
            for (let i = displayLimit; i < orderList.length; i++) {
                betCount += orderList[i].BetCount;
            }
            this.betCountBySportData.push(betCount);
        }
    }

    private generateWinLossBySports() {
        const sportStatistics = this.statistic.SportStatistics;
        const orderList = sportStatistics.sort((a, b) => {
            if (a.WinLoss > b.WinLoss) {
                return 1;
            } else if (a.WinLoss === b.WinLoss) {
                return 0;
            }
            return -1;
        });
        const trimList = orderList.slice(0, displayLimit);
        this.winLossLabels = trimList.map(item => item.SportName);
        const winLossData = trimList.map(item => item.WinLoss);
        if (orderList.length > displayLimit) {
            this.winLossLabels.push(groupName);
            let winLoss = 0;
            for (let i = displayLimit; i < orderList.length; i++) {
                winLoss += orderList[i].WinLoss;
            }
            winLossData.push(winLoss);
        }
        this.winLossDatasets = [{
            data: winLossData,
            label: 'Winloss'
        }];
    }

    private generateTurnoverBySports() {
        const sportStatistics = this.statistic.SportStatistics;
        const orderList = sportStatistics.sort((a, b) => {
            if (a.Turnover > b.Turnover) {
                return -1;
            } else if (a.Turnover === b.Turnover) {
                return 0;
            }
            return 1;
        });
        const trimList = orderList.slice(0, displayLimit);
        this.turnoverLabels = trimList.map(item => item.SportName);
        this.turnoverData = trimList.map(item => item.Turnover);
        if (orderList.length > displayLimit) {
            this.turnoverLabels.push(groupName);
            let turnover = 0;
            for (let i = displayLimit; i < orderList.length; i++) {
                turnover += orderList[i].Turnover;
            }
            this.turnoverData.push(turnover);
        }
    }
}
