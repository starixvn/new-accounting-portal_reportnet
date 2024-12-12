using AutoMapper;
using BetCount.Constants;
using BetCount.Criteria;
using BetCount.Entities;
using BetCount.Model;
using Fanex.Data.Repository;
using Utilities.Sorting;
using Utilities.LinqDynamicSum;
using Utilities.RemotingRef;

namespace BetCount.Services
{
    public interface BetCountService
    {
        Task<TransactionModel> GetReport(BetCountCriteria criteria);

        Task<TransactionModel> SortReport(BetCountCriteria criteria);

        Task<List<ProductModel>> GetProduct(ProductCriteria criteria);

        Task<List<ProductModel>> GetSport(ProductCriteria criteria);
    }

    public class BetCountServiceImp(
        IDynamicRepository repository,
        RemotingRefService remotingRefService,
        IMapper mapper) : BetCountService
    {
        public async Task<TransactionModel> GetReport(BetCountCriteria criteria)
        {
            var rawData = await repository.FetchAsync<BetCountEntity>(criteria);

            var report = new TransactionModel
            {
                Items = [],
                Total = new TransactionModel
                {
                    SiteName = "Total"
                }
            };

            rawData.GroupBy(data => data.IsDeposit)
                   .ToList()
                   .ForEach(group =>
                   {
                       var transByIsDepositionGroup = new TransactionModel
                       {
                           Items = [],
                           Total = new TransactionModel(),
                           SiteName = group.Key ? "Deposite" : "Credit"
                       };

                       transByIsDepositionGroup.Total.SiteName = transByIsDepositionGroup.SiteName;

                       group.ToList()
                            .ForEach(transaction =>
                            {
                                var transactionData = mapper.Map<BetCountEntity, TransactionModel>(transaction);
                                transByIsDepositionGroup.Items.Add(transactionData);
                            });

                       var groupTotal = transByIsDepositionGroup.Items.DynamicSum();
                       groupTotal.SiteName = transByIsDepositionGroup.SiteName;
                       transByIsDepositionGroup.Total = groupTotal;

                       report.Items.Add(transByIsDepositionGroup);
                   });


            var total = report.Items.Select(x => x.Total).DynamicSum();
            total.SiteName = "Total";
            report.Total = total;

            return report;
        }

        public async Task<TransactionModel> SortReport(BetCountCriteria criteria)
        {
            var report = await GetReport(criteria);

            if (report != null &&
                report.Items != null &&
                report.Items.Count > 0)
            {
                var siteGroupLength = report.Items.Count;
                for (var i = 0; i < siteGroupLength; i++)
                {
                    var propertyName = GetSortedColumnName(criteria.SortBy);
                    report.Items[i].Items.SortByProperty(
                        criteria.SortDirection,
                        propertyName);
                }
            }

            return report;
        }

        public async Task<List<ProductModel>> GetProduct(ProductCriteria criteria)
        {
            criteria ??= new ProductCriteria();

            var rawData = await repository.FetchAsync<ProductEntity>(criteria);

            List<ProductModel> products = [];

            var productGroups = rawData.GroupBy(rawInfo => rawInfo.ProductName);

            foreach (var productGroup in productGroups)
            {
                var product = new ProductModel
                {
                    Name = productGroup.Key,
                    Items = []
                };

                if (BetCountConstants.ProductsHaveSports.Contains(product.Name.ToLower()))
                {
                    products.Add(product);
                    continue;
                }

                var sportGroups = productGroup.GroupBy(x => new { x.SportId, x.SportName });

                foreach (var sportGroup in sportGroups)
                {
                    var sport = CreateSport(sportGroup.Key);
                    foreach (var data in sportGroup)
                    {
                        sport.Items.Add(CreateProduct(data));
                    }

                    product.Items.Add(sport);
                }

                products.Add(product);
            }

            return products;
        }

        public async Task<List<ProductModel>> GetSport(ProductCriteria criteria)
        {
            criteria ??= new ProductCriteria();

            var rawData = (await repository.FetchAsync<ProductEntity>(criteria)).ToList();

            var sports = new List<ProductModel>();

            CalculateSportData(rawData, sports);

            return sports;
        }

        private string GetSortedColumnName(string sortBy)
        {
            var columnName = string.Empty;

            switch (sortBy)
            {
                case "TotalTurnover":
                    columnName = "Turnover";
                    break;

                case "MemberWinLoss":
                    columnName = "MemberWinLoss";
                    break;

                case "MarginPercent":
                    columnName = "MarginPercent";
                    break;

                case "AgentWinLoss":
                    columnName = "AgentWinLoss";
                    break;

                case "MasterWinLoss":
                    columnName = "MasterWinLoss";
                    break;

                case "SuperWinLoss":
                    columnName = "SuperWinLoss";
                    break;

                case "TotalWinLoss":
                    columnName = "TotalWinLoss";
                    break;

                case "CompanyWinLoss":
                    columnName = "CompanyWinLoss";
                    break;

                case "Commission":
                    columnName = "Commission";
                    break;

                case "ExtraPT":
                    columnName = "ExtraPT";
                    break;
            }

            return columnName;
        }

        private ProductModel GenerateSports(IEnumerable<ProductEntity> rawData)
        {
            if (!rawData.Any())
            {
                return new ProductModel
                {
                    Id = 0,
                    Items = []
                };
            }

            var indGroup = new ProductModel
            {
                Id = rawData.FirstOrDefault().SportId,
                Items = []
            };

            indGroup.Name = GetSportName(indGroup.Id);
            indGroup.Id = GetGroupId(indGroup.Id);

            foreach (var data in rawData)
            {
                indGroup.Items.Add(CreateProduct(data));
            }

            return indGroup;
        }

        private int GetGroupId(int id)
        {
            if (id == BetCountConstants.MiscSoccerGroupId ||
                id == BetCountConstants.MiscParLayGroupId ||
                id == BetCountConstants.MiscBasketballGroupId ||
                id == BetCountConstants.MiscEsportsGroupId ||
                id == BetCountConstants.SBAdjustmentId)
            {
                return id;
            }
            else
            {
                return -111;
            }

        }

        private ProductModel GenerateOtherSports(IEnumerable<ProductEntity> rawData)
        {
            var indGroup = CreateGroup(BetCountConstants.MiscOtherSportId);
            indGroup.Name = "Others";
            var sportGroups = rawData.GroupBy(entity => new { entity.SportId, entity.SportName });
            foreach (var sportGroup in sportGroups)
            {
                var sport = CreateSport(sportGroup.Key);
                foreach (var data in sportGroup)
                {
                    sport.Items.Add(CreateProduct(data));
                }

                indGroup.Items.Add(sport);
            }

            return indGroup;
        }

        private ProductModel CreateSport(dynamic sportGroup)
        {
            return new ProductModel
            {
                Id = sportGroup.SportId,
                Name = sportGroup.SportName,
                Items = []
            };
        }

        private ProductModel CreateProduct(dynamic data)
        {
            return new ProductModel
            {
                Id = data.Bettype,
                Name = data.BettypeName
            };
        }

        private ProductModel CreateGroup(int key)
        {
            return new ProductModel
            {
                Id = key,
                Items = []
            };
        }

        private ProductModel AddSportGroup(IGrouping<dynamic, dynamic> rawData)
        {
            var indGroup = CreateGroup(rawData.Key.SportId);
            indGroup.Name = rawData.Key.SportName;
            foreach (var data in rawData)
            {
                indGroup.Items.Add(CreateProduct(data));
            }

            return indGroup;
        }

        private void CalculateSportData(IList<ProductEntity> rawData, List<ProductModel> sports)
        {
            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscSportbookId))
            {
                CreateSportBookGroups(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscVirtualSportsId))
            {
                CreateVirtualSportGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscNG2ProductID))
            {
                CreateNG2SportGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == 3))
            {
                CreateNGSportGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscBetradarVirtualSportsId))
            {
                CreateBetradarVirtualSportGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.BetBuilderProductID))
            {
                CreateBetBuilderSportGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.SabaClubProductId))
            {
                CreateSportGroup(rawData, sports, BetCountConstants.SabaClubProductId);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.VGaming))
            {
                CreateSportGroup(rawData, sports, BetCountConstants.VGaming);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscVirtualSportsThirdId))
            {
                CreateVirtualSportThirdGroup(rawData, sports);
            }

            if (rawData.Any(x => x.ProductId == BetCountConstants.MiscVirtualSportsThirdIdOld))
            {
                CreateVirtualSportThirdOldGroup(rawData, sports);
            }
        }

        private void CreateSportBookGroups(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var sportbookRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscSportbookId);
            var soccerGroupData = sportbookRaw.Where(r => r.SportId == BetCountConstants.MiscSoccerGroupId);
            var otherSportGroupData = sportbookRaw.Where(r =>
                r.SportId != BetCountConstants.MiscSoccerGroupId
                && r.SportId != BetCountConstants.MiscParLaySportId
                && r.SportId != BetCountConstants.MiscBasketballGroupId
                && r.SportId != BetCountConstants.MiscEsportsGroupId
                && r.SportId != BetCountConstants.SBAdjustmentId);

            var parlayGroupData = sportbookRaw.Where(r => r.SportId == BetCountConstants.MiscParLaySportId);
            var basketballGroupData = sportbookRaw.Where(r => r.SportId == BetCountConstants.MiscBasketballGroupId);
            var esportsGroupData = sportbookRaw.Where(r => r.SportId == BetCountConstants.MiscEsportsGroupId);
            var sbAdjustmentGroupData = sportbookRaw.Where(r => r.SportId == BetCountConstants.SBAdjustmentId);

            sports.Add(GenerateSports(soccerGroupData));
            sports.Add(GenerateSports(basketballGroupData));
            sports.Add(GenerateSports(esportsGroupData));
            sports.Add(GenerateOtherSports(otherSportGroupData));
            sports.Add(GenerateSports(parlayGroupData));
            sports.Add(GenerateSports(sbAdjustmentGroupData));
        }

        private void CreateVirtualSportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var virtualSportRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscVirtualSportsId);
            var virtualSportGroups = virtualSportRaw.GroupBy(x => new { x.SportId, x.SportName });
            foreach (var virtualSportGroup in virtualSportGroups)
            {
                sports.Add(AddSportGroup(virtualSportGroup));
            }
        }

        private void CreateBetradarVirtualSportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var virtualSportRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscBetradarVirtualSportsId);
            var virtualSportGroups = virtualSportRaw.GroupBy(rawInfo => new { rawInfo.SportId, rawInfo.SportName });
            foreach (var virtualSportGroup in virtualSportGroups)
            {
                sports.Add(AddSportGroup(virtualSportGroup));
            }
        }

        private void CreateNG2SportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var numberGame2sportRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscNG2ProductID);
            var numberGame2sportGroups = numberGame2sportRaw.GroupBy(rawInfo => new { rawInfo.SportId, rawInfo.SportName });
            foreach (var numberGame2sportGroup in numberGame2sportGroups)
            {
                sports.Add(AddSportGroup(numberGame2sportGroup));
            }
        }

        private void CreateNGSportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var numberGameSportRaw = rawData.Where(r => r.ProductId == 3);
            var numberGameSportGroups = numberGameSportRaw.GroupBy(rawInfo => new { rawInfo.SportId, rawInfo.SportName });
            foreach (var numberGameSportGroup in numberGameSportGroups)
            {
                sports.Add(AddSportGroup(numberGameSportGroup));
            }
        }

        private void CreateBetBuilderSportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var betBuilderRaw = rawData.Where(r => r.ProductId == BetCountConstants.BetBuilderProductID);
            var betBuilderSportGroups = betBuilderRaw.GroupBy(entity => new { entity.SportId, entity.SportName });
            foreach (var group in betBuilderSportGroups)
            {
                if (sports.FirstOrDefault(x => x.Name == group.Key.SportName) != null)
                {
                    foreach (var item in group)
                    {
                        sports.FirstOrDefault(x => x.Name == group.Key.SportName).Items.Add(new ProductModel
                        {
                            Id = item.Bettype,
                            Name = item.BettypeName
                        });
                    }
                }
                else
                {
                    sports.Add(AddSportGroup(group));
                }
            }
        }

        private void CreateSportGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports, int productId)
        {
            var sportRaw = rawData.Where(r => r.ProductId == productId);
            var sportGroups = sportRaw.GroupBy(entity => new { entity.SportId, entity.SportName });
            foreach (var sportGroup in sportGroups)
            {
                sports.Add(AddSportGroup(sportGroup));
            }
        }

        private string GetSportName(int sportId)
        {
            switch (sportId)
            {
                case BetCountConstants.MiscSoccerGroupId:
                case BetCountConstants.MiscBasketballGroupId:
                    return remotingRefService.GetSportName(sportId);

                case BetCountConstants.MiscParLayGroupId:
                    return "Parlay";

                case BetCountConstants.MiscEsportsGroupId:
                    return "E-Sports";

                case BetCountConstants.SBAdjustmentId:
                    return "SB Adjustment";
            }

            return string.Empty;
        }

        private List<ProductModel> CreateVirtualSportThirdGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var virtualSportRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscVirtualSportsThirdId);
            var virtualSportGroups = virtualSportRaw.GroupBy(entity => new { entity.SportId, entity.SportName });
            foreach (var virtualSportGroup in virtualSportGroups)
            {
                sports.Add(AddSportGroup(virtualSportGroup));
            }

            return sports;
        }

        private List<ProductModel> CreateVirtualSportThirdOldGroup(IEnumerable<ProductEntity> rawData, List<ProductModel> sports)
        {
            var virtualSportRaw = rawData.Where(r => r.ProductId == BetCountConstants.MiscVirtualSportsThirdIdOld);
            var virtualSportGroups = virtualSportRaw.GroupBy(entity => new { entity.SportId, entity.SportName });
            foreach (var virtualSportGroup in virtualSportGroups)
            {
                sports.Add(AddSportGroup(virtualSportGroup));
            }

            return sports;
        }
    }
}
