import { Injectable } from '@angular/core';
import { LiveStatus, GridType } from './grid.model';

@Injectable()
export class CommonService {
    getLiveStatusTitle(liveStatus: LiveStatus): string {
        switch (liveStatus) {
            case LiveStatus.Live:
                return 'Live';
            case LiveStatus.NonLive:
                return 'Non-Live';
            default:
                return 'Non-Live & Live';
        }
    }

    getGridTypeTitle(gridType: GridType): string {
        switch (gridType) {
            case GridType.UserName:
                return 'Summary';
            case GridType.SportType:
                return 'Sporttype';
            case GridType.BetType:
            case GridType.GameBetType:
                return 'Bettype';
            case GridType.League:
                return 'League';
            case GridType.BetCount:
                return 'Betcount';
            case GridType.AssociatedAccount:
                return 'AssociatedAccount';
            case GridType.Game:
                return 'LeagueGroup';
            case GridType.BetTypeGroup:
                return 'BettypeGroup';
            default:
                return '';
        }
    }
}
