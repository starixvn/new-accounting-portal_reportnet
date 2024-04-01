namespace Nap_Reports.Utilities.Header
{
    public class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public List<Menu> Children { get; set; }
    }
}
