interface BalanceUpRange {
  balanceUpDay: string;
  moveBetsDay: string;
}

export interface BalanceUpModel {
  today: string;
  balanceUpRange: BalanceUpRange;
}
