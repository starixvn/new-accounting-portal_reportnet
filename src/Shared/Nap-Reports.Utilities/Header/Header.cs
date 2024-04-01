namespace Nap_Reports.Utilities.Header
{
    public class Header
    {
        public string LegacyPath { get; set; } = string.Empty;

        public string Username { get; set; }

        public IList<Menu> Menus { get; set; }
    }
}
