using BetList.Criteria;
using BetList.Model;
using Fanex.Data.Repository;
using Fanex.Result.Core;
using Fanex.Result.Core.Base;
using Fanex.Result.Core.Sportsbook.DTO;

namespace BetList.Services
{
    public interface ScoreInformationService
    {
        Task<ScoreInformation> GetScoreInfo(GetScoreInformationCriteria criteria);
    }
    public class ScoreInformationServiceImp : ScoreInformationService
    {
        private readonly IDynamicRepository repository;

        public ScoreInformationServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ScoreInformation> GetScoreInfo(GetScoreInformationCriteria criteria)
        {
            var scoreInformation = new ScoreInformation();
            var rawData = await repository.FetchMultipleAsync<Sportsbook, GameDetail>(criteria);
            var sportsBooks = BindingScoreInformation(rawData);
            scoreInformation.MatchId = criteria.MatchId;
            scoreInformation.TraderName = criteria.TraderName;
            var resultSettings = GetFanexResultSetting(criteria);


            if (criteria.SportType != (int)SportType.Tennis || sportsBooks[0].GameDetails[0].MatchId == 0)
            {
                scoreInformation.MatchInformation = ResultEngine.HtmlParser(sportsBooks, resultSettings).Replace("original-title", " title ");
                return scoreInformation;
            }

            scoreInformation.MatchInformation = ResultEngine.HtmlGameDetailParser(sportsBooks[0], resultSettings);
            return scoreInformation;
        }

        private ResultSetting GetFanexResultSetting(GetScoreInformationCriteria criteria)
        {
            return new ResultSetting
            {
                SportType = (SportType)criteria.SportType,
                IsOutright = false,
                IsParlayPopUp = false,
                IsPopUp = true,
                LanguageCode = "en-US"
            };
        }

        public List<Sportsbook> BindingScoreInformation((IEnumerable<Sportsbook>, IEnumerable<GameDetail>) rawData)
        {
            var result = new List<Sportsbook>();

            if(rawData.Item1 != null && rawData.Item1.Any())
            {
                var item = rawData.Item1.FirstOrDefault();

                if (rawData.Item2 != null && rawData.Item2.Any())
                {
                    item.GameDetails.AddRange(rawData.Item2);
                }
                else
                {
                    item.GameDetails.Add(new GameDetail());
                }

                result.Add(item);
            }

            return result;
        }
    }
}
