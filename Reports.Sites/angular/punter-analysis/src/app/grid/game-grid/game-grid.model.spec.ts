import { GameData } from './game-grid.model';
export let fakeData: GameData = {
    Entities: [
        {
            LeagueGroupId: 1,
            LeagueGroupName: 'group 1',
            WinLoss: 13024.2343,
            Turnover: 2490.1800,
            Margin: 5.2302380952380952380952380952,
            BetCount: 10
        },
        {
            LeagueGroupId: 2,
            LeagueGroupName: 'group 2',
            WinLoss: 12337.5491,
            Turnover: 2712.8971,
            Margin: 4.5477394258705942072038043758,
            BetCount: 31
        }
    ],
    Total: {
        LeagueGroupId: 0,
        LeagueGroupName: '',
        WinLoss: 25361.7834,
        Turnover: 5203.0771,
        Margin: 4.8743816231360477053088450294,
        BetCount: 41
    }
};
