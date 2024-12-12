using Fanex.Data;
using Fanex.Data.Repository;
using TotalSMAAndActiveMember.Constant;

namespace TotalSMAAndActiveMember.Criteria

{
    public class StatusDetailsCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SiteList { get; set; }

        // for title of detail report
        [SpParam(Ignored = true)]
        public string SiteName { get; set; }

        public int RoleId { get; set; }

        public bool ShowDetailNew
        {
            get => Status == (int)StatusConstant.NewCreated;

            set
            {
            }
        }

        public bool ShowDetailReject
        {
            get => Status == (int)StatusConstant.Suspended;

            set
            {
            }
        }

        public bool ShowDetailClosed
        {
            get => Status == (int)StatusConstant.Closed;

            set
            {
            }
        }

        #region Paging params

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRow { get; set; }

        #endregion Paging params

        #region Client params

        [SpParam(Ignored = true)]
        public int Status { get; set; }

        #endregion Client params

        public override string GetSettingKey()
        {
            return "Misc_TotalSMAMemberActive_GetStatusDetail";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
