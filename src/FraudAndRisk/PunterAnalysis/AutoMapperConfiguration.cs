namespace PunterAnalysis;

using AssociatedAccount.Model;
using AutoMapper;
using BetCount.Model;
using BetType.Model;
using BetTypeGroup.Model;
using Filter.UsernameSuggestion;
using Game.Model;
using GameBetType.Model;
using Performance.Sportsbook;
using Performance.Summary;
using Performance.Summary.Criteria;
using Shared;
using SportType.Model;
using Summary.Model;
using Utilities.LinqDynamicSum;
using Criteria = BetCount.Model.Criteria;
using HttpParameterExcel = BetCount.Model.HttpParameterExcel;
using HttpParameterJson = BetCount.Model.HttpParameterJson;
using SportCriteria = Performance.Sportsbook.Criteria.SportCriteria;

public static class AutoMapperConfiguration
{
    public static void InitializePunterAnalysis(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetCountModel, BetCountExcelModel>();
        cfg.CreateMap<BetTypeModel, BetTypeExcelModel>();
        cfg.CreateMap<SportTypeModel, SportTypeExcelModel>();
        cfg.CreateMap<SummaryModel, SummaryExcelModel>();
        cfg.CreateMap<GameModel, GameExcelModel>();
        cfg.CreateMap<GameBetTypeModel, GameBetTypeExcelModel>();
        cfg.CreateMap<BetTypeGroupModel, BetTypeGroupExcelModel>();

        cfg.CreateMap<SummaryEntity<BetCountModel>, SummaryEntity<BetCountExcelModel>>();
        cfg.CreateMap<SummaryEntity<BetTypeModel>, SummaryEntity<BetTypeExcelModel>>();
        cfg.CreateMap<SummaryEntity<SportTypeModel>, SummaryEntity<SportTypeExcelModel>>();
        cfg.CreateMap<SummaryEntity<SummaryModel>, SummaryEntity<SummaryExcelModel>>();
        cfg.CreateMap<SummaryEntity<GameModel>, SummaryEntity<GameExcelModel>>();
        cfg.CreateMap<SummaryEntity<GameBetTypeModel>, SummaryEntity<GameBetTypeExcelModel>>();
        cfg.CreateMap<SummaryEntity<BetTypeGroupModel>, SummaryEntity<BetTypeGroupExcelModel>>();
        cfg.CreateMap<SummaryEntity<AssociatedSummaryModel>, SummaryEntity<AssociatedSummaryExcelModel>>();
        cfg.CreateMap<HttpParameterExcel, Criteria>();
        cfg.CreateMap<HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<HttpParameterJson, Criteria>();
        cfg.CreateMap<BetType.Model.HttpParameterExcel, BetType.Model.Criteria>();
        cfg.CreateMap<BetType.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<BetType.Model.HttpParameterJson, BetType.Model.Criteria>();
        cfg.CreateMap<Filter.UsernameSuggestion.HttpParameterJson, QueryCriteria>();
        cfg.CreateMap<League.Model.HttpParameterExcel, League.Model.Criteria>();
        cfg.CreateMap<League.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<League.Model.HttpParameterJson, League.Model.Criteria>();
        cfg.CreateMap<SportsbookHttpParameterJson, SportCriteria>();
        cfg.CreateMap<SummaryHttpParameterJson, BetTypeCriteria>();
        cfg.CreateMap<SummaryHttpParameterJson, EventCriteria>();
        cfg.CreateMap<SummaryHttpParameterJson, MonthCriteria>();
        cfg.CreateMap<SummaryHttpParameterJson, ProductTurnoverCriteria>();
        cfg.CreateMap<SummaryHttpParameterJson, Performance.Summary.Criteria.SportCriteria>();
        cfg.CreateMap<SportType.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<SportType.Model.HttpParameterExcel, SportType.Model.Criteria>();
        cfg.CreateMap<SportType.Model.HttpParameterJson, SportType.Model.Criteria>();
        cfg.CreateMap<Summary.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<Summary.Model.HttpParameterExcel, Summary.Model.Criteria>();
        cfg.CreateMap<Summary.Model.HttpParameterJson, Summary.Model.Criteria>();
        cfg.CreateMap<AssociatedSummaryModel, AssociatedSummaryExcelModel>();
        cfg.CreateMap<AssociatedAccount.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<Game.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<Game.Model.HttpParameterExcel, Game.Model.Criteria>();
        cfg.CreateMap<Game.Model.HttpParameterJson, Game.Model.Criteria>();

        cfg.CreateMap<GameBetType.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<GameBetType.Model.HttpParameterExcel, GameBetType.Model.Criteria>();
        cfg.CreateMap<GameBetType.Model.HttpParameterJson, GameBetType.Model.Criteria>();

        cfg.CreateMap<BetTypeGroup.Model.HttpParameterExcel, ExcelSettingParams>();
        cfg.CreateMap<BetTypeGroup.Model.HttpParameterExcel, BetTypeGroup.Model.Criteria>();
        cfg.CreateMap<BetTypeGroup.Model.HttpParameterJson, BetTypeGroup.Model.Criteria>();

    }
}