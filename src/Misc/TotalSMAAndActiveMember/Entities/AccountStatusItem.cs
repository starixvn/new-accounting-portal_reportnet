namespace TotalSMAAndActiveMember.Entities
{
    public class AccountStatusItem
    {
        public int NewCreatedSuper { get; set; }

        public int NewCreatedMaster { get; set; }

        public int NewCreatedAgent { get; set; }

        public int ActiveSuper { get; set; }

        public int ActiveMaster { get; set; }

        public int ActiveAgent { get; set; }

        public int SuspendedSuper { get; set; }

        public int SuspendedMaster { get; set; }

        public int SuspendedAgent { get; set; }

        public int ClosedSuper { get; set; }

        public int ClosedMaster { get; set; }

        public int ClosedAgent { get; set; }
    }
}
