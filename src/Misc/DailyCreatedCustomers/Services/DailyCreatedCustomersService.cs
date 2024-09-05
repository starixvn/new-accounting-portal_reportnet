//using DailyCreatedCustomers.Criteria;
//using DailyCreatedCustomers.Model;

//namespace Fanex.AccountingReports.Misc.Services.Implement
//{
//    public class DailyCreatedCustomersServiceImp
//    {
//        public SummaryModel GetReport(DailyCreatedCustomersCriteria criteria)
//        {
//            var report = new SummaryModel
//            {
//                CreatingDetails = new List<SummaryModel>(),
//            };

//            if (!criteria.IsValid())
//            {
//                return report;
//            }

//            var rawData = GetReportData<dynamic>(criteria);
//            foreach (var row in rawData)
//            {
//                DateTime createdDate = Converter.ToDateTime(row.CreateDate);
//                SummaryModel info = new SummaryModel
//                {
//                    CreatedDate = createdDate.ToShortFormatedDate(),
//                    SuperAmount = Converter.ToInt(row.AmountSAg),
//                    DirectMasterAmount = Converter.ToInt(row.AmountMAg),
//                    DirectAgentAmount = Converter.ToInt(row.AmountAg),
//                    MasterAmount = Converter.ToInt(row.AmountMaster),
//                    AgentAmount = Converter.ToInt(row.AmountAgent),
//                    MemberAmount = Converter.ToInt(row.AmountMember)
//                };

//                report.SumDataToTotal(info);
//                report.CreatingDetails.Add(info);
//            }

//            return report;
//        }

//        public List<DetailModel> GetReportDetails(DailyCreatedCustomersDetailsCriteria criteria)
//        {
//            var report = new List<DetailModel>();
//            if (!criteria.IsValid())
//            {
//                return report;
//            }

//            var rawData = GetReportData<dynamic>(criteria);
//            foreach (var row in rawData)
//            {
//                DateTime createdDate = Converter.ToDateTime(row.creatdate);
//                DetailModel info = new DetailModel
//                {
//                    CreatedDate = createdDate.ToLongFormatedDate(),
//                    UserId = Converter.ToInt(row.custid),
//                    UserName = Converter.ToString2(row.username),
//                    Creator = Converter.ToString2(row.creator)
//                };

//                report.Add(info);
//            }

//            return report;
//        }
//    }
//}
