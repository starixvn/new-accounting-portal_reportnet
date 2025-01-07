using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Models;
using Newtonsoft.Json;

namespace HouseWinLoss.SimpleGrid;

public interface SimpleCriteriaFactory
{
    WinLossCriteria CreateCriteria(Dictionary<string, object> queryModel);

    GridDecorationModel CreateGridDecorationModel(Dictionary<string, object> queryModel);
}

public class SimpleCriteriaFactoryImp : SimpleCriteriaFactory
{
    private static readonly Dictionary<string, Type> CriteriaTypes = new()
    {
        { "universal-second-level", typeof(UniversalSecondLevelCriteria)},
        { "number-game-drill-down", typeof(NumberGameDrilldownCriteria)},
        { "universal-thirdparty-bettype", typeof(UniversalThirdPartyBetTypeCriteria)},
        { "universal-thirdparty-site", typeof(UniversalThirdPartySiteCriteria)},
        { "universal-thirdparty-matchdetail", typeof(UniversalThirdPartyMatchDetailCriteria)}
    };

    public WinLossCriteria CreateCriteria(Dictionary<string, object> queryModel)
    {
        var criteriaType = queryModel["type"].ToString() ?? string.Empty;

        if (!CriteriaTypes.TryGetValue(criteriaType, out var targetType))
        {
            throw new ArgumentException("queryModel[\"type\"]");
        }

        var criteria = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryModel), targetType);

        return (WinLossCriteria)criteria;
    }

    public GridDecorationModel CreateGridDecorationModel(Dictionary<string, object> queryModel)
    {
        return JsonConvert.DeserializeObject<GridDecorationModel>(JsonConvert.SerializeObject(queryModel));
    }
}