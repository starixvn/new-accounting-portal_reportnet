namespace Nap_Reports.Utilities.LinqDynamicSum
{
    public class SummaryEntity<T>
    {
        public List<T> Entities { get; set; } = new List<T>();

        public T Total { get; set; }
    }
}
