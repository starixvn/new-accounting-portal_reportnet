using AutoMapper;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis
{
    public static class AutoMapperConfiguration
    {
        public static void InitializePunterAnalysis(this IProfileExpression cfg)
        {
            cfg.CreateMap<BetCount.Model.BetCountModel, BetCount.Model.BetCountExcelModel>();
            cfg.CreateMap<BetType.Model.BetTypeModel, BetType.Model.BetTypeExcelModel>();
            cfg.CreateMap<SportType.Model.SportTypeModel, SportType.Model.SportTypeExcelModel>();
            cfg.CreateMap<Summary.Model.SummaryModel, Summary.Model.SummaryExcelModel>();
            cfg.CreateMap<Game.Model.GameModel, Game.Model.GameExcelModel>();
            cfg.CreateMap<GameBetType.Model.GameBetTypeModel, GameBetType.Model.GameBetTypeExcelModel>();
            cfg.CreateMap<BetTypeGroup.Model.BetTypeGroupModel, BetTypeGroup.Model.BetTypeGroupExcelModel>();

            cfg.CreateMap<SummaryEntity<BetCount.Model.BetCountModel>, SummaryEntity<BetCount.Model.BetCountExcelModel>>();
            cfg.CreateMap<SummaryEntity<BetType.Model.BetTypeModel>, SummaryEntity<BetType.Model.BetTypeExcelModel>>();
            cfg.CreateMap<SummaryEntity<SportType.Model.SportTypeModel>, SummaryEntity<SportType.Model.SportTypeExcelModel>>();
            cfg.CreateMap<SummaryEntity<Summary.Model.SummaryModel>, SummaryEntity<Summary.Model.SummaryExcelModel>>();
            cfg.CreateMap<SummaryEntity<Game.Model.GameModel>, SummaryEntity<Game.Model.GameExcelModel>>();
            cfg.CreateMap<SummaryEntity<GameBetType.Model.GameBetTypeModel>, SummaryEntity<GameBetType.Model.GameBetTypeExcelModel>>();
            cfg.CreateMap<SummaryEntity<BetTypeGroup.Model.BetTypeGroupModel>, SummaryEntity<BetTypeGroup.Model.BetTypeGroupExcelModel>>();
            cfg.CreateMap<SummaryEntity<AssociatedAccount.Model.AssociatedSummaryModel>, SummaryEntity<AssociatedAccount.Model.AssociatedSummaryExcelModel>>();
            cfg.CreateMap<BetCount.Model.HttpParameterExcel, BetCount.Model.Criteria>();
            cfg.CreateMap<BetCount.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<BetCount.Model.HttpParameterJson, BetCount.Model.Criteria>();
            cfg.CreateMap<BetType.Model.HttpParameterExcel, BetType.Model.Criteria>();
            cfg.CreateMap<BetType.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<BetType.Model.HttpParameterJson, BetType.Model.Criteria>();
            cfg.CreateMap<Filter.UsernameSuggestion.HttpParameterJson, Filter.UsernameSuggestion.QueryCriteria>();
            cfg.CreateMap<League.Model.HttpParameterExcel, League.Model.Criteria>();
            cfg.CreateMap<League.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<League.Model.HttpParameterJson, League.Model.Criteria>();
            cfg.CreateMap<Performance.Sportsbook.SportsbookHttpParameterJson, Performance.Sportsbook.Criteria.SportCriteria>();
            cfg.CreateMap<Performance.Summary.SummaryHttpParameterJson, Performance.Summary.Criteria.BetTypeCriteria>();
            cfg.CreateMap<Performance.Summary.SummaryHttpParameterJson, Performance.Summary.Criteria.EventCriteria>();
            cfg.CreateMap<Performance.Summary.SummaryHttpParameterJson, Performance.Summary.Criteria.MonthCriteria>();
            cfg.CreateMap<Performance.Summary.SummaryHttpParameterJson, Performance.Summary.Criteria.ProductTurnoverCriteria>();
            cfg.CreateMap<Performance.Summary.SummaryHttpParameterJson, Performance.Summary.Criteria.SportCriteria>();
            cfg.CreateMap<SportType.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<SportType.Model.HttpParameterExcel, SportType.Model.Criteria>();
            cfg.CreateMap<SportType.Model.HttpParameterJson, SportType.Model.Criteria>();
            cfg.CreateMap<Summary.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<Summary.Model.HttpParameterExcel, Summary.Model.Criteria>();
            cfg.CreateMap<Summary.Model.HttpParameterJson, Summary.Model.Criteria>();
            cfg.CreateMap<AssociatedAccount.Model.AssociatedSummaryModel, AssociatedAccount.Model.AssociatedSummaryExcelModel>();
            cfg.CreateMap<AssociatedAccount.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<Game.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<Game.Model.HttpParameterExcel, Game.Model.Criteria>();
            cfg.CreateMap<Game.Model.HttpParameterJson, Game.Model.Criteria>();

            cfg.CreateMap<GameBetType.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<GameBetType.Model.HttpParameterExcel, GameBetType.Model.Criteria>();
            cfg.CreateMap<GameBetType.Model.HttpParameterJson, GameBetType.Model.Criteria>();

            cfg.CreateMap<BetTypeGroup.Model.HttpParameterExcel, Shared.ExcelSettingParams>();
            cfg.CreateMap<BetTypeGroup.Model.HttpParameterExcel, BetTypeGroup.Model.Criteria>();
            cfg.CreateMap<BetTypeGroup.Model.HttpParameterJson, BetTypeGroup.Model.Criteria>();

        }
    }
}
