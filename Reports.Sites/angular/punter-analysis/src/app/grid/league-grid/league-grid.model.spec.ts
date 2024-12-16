import { LeagueData } from './league-grid.model';
export let fakeData: LeagueData = {
  GroupNames: [
    'Year 2016',
    'December',
    'Year 2017',
    'January',
    'Grand Total'
  ],
  Entities: [
    {
      LeagueName: 'League 1',
      PeriodGroups: [
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        },
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        },
        {
          BetCount: 0,
          WinLoss: 0,
          Turnover: 0,
          Margin: 0
        },
        {
          BetCount: 0,
          WinLoss: 0,
          Turnover: 0,
          Margin: 0
        },
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        }
      ]
    },
    {
      LeagueName: 'League 2',
      PeriodGroups: [
        {
          BetCount: 0,
          WinLoss: 0,
          Turnover: 0,
          Margin: 0
        },
        {
          BetCount: 0,
          WinLoss: 0,
          Turnover: 0,
          Margin: 0
        },
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        },
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        },
        {
          BetCount: 5,
          WinLoss: 100,
          Turnover: 500,
          Margin: 0.2
        }
      ]
    }
  ],
  Total: {
    LeagueName: '',
    PeriodGroups: [
      {
        BetCount: 5,
        WinLoss: 100,
        Turnover: 500,
        Margin: 0.2
      },
      {
        BetCount: 5,
        WinLoss: 100,
        Turnover: 500,
        Margin: 0.2
      },
      {
        BetCount: 5,
        WinLoss: 100,
        Turnover: 500,
        Margin: 0.2
      },
      {
        BetCount: 5,
        WinLoss: 100,
        Turnover: 500,
        Margin: 0.2
      },
      {
        BetCount: 10,
        WinLoss: 200,
        Turnover: 1000,
        Margin: 0.2
      }
    ]
  }
};
