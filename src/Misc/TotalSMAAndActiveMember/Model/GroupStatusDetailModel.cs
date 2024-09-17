namespace TotalSMAAndActiveMember.Model
{
    public class GroupStatusDetailModel
    {
        public int Id {  get; set; }

        public string Name { get; set; }    

        public List<StatusDetailModel> ChildItems { get; set; }
    }
}
