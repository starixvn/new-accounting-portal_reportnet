namespace Utilities.LinqDynamicSum
{
    public class SummaryEntity<T>
    {
        public List<T> Entities { get; set; } = [];

        public T Total { get; set; }
    }
}
