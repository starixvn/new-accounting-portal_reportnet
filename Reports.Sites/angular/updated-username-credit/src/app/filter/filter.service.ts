import { SelectItem } from 'nap-components/dropdown-select';
import { TimePeriod } from './filter.model';

export class FilterService {
    getTimeSelectItems(): SelectItem[] {
        return [
            { text: 'Today', value: TimePeriod.Today },
            { text: 'Yesterday', value: TimePeriod.Yesterday },
            { text: 'Current Week', value: TimePeriod.CurrentWeek },
            { text: 'Last Week', value: TimePeriod.LastWeek },
            { text: 'Current Month', value: TimePeriod.CurrentMonth },
            { text: 'Last Month', value: TimePeriod.LastMonth },
            { text: 'Since Last Month', value: TimePeriod.SinceLastMonth },
            { text: 'Specific Date', value: TimePeriod.SpecificDate },
            { text: 'Last 3 Months', value: TimePeriod.LastThreeMonths },
            { text: 'Last 6 Months', value: TimePeriod.LastSixMonths }
        ];
    }
}
