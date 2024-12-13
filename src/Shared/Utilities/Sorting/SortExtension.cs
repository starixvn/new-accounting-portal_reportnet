namespace Utilities.Sorting
{
    public static class SortExtension
    {
        public static void SortByProperty<T>(this List<T> dataSource, string sortType, string sortCol)
        {
            var propInfo = typeof(T).GetProperty(sortCol);
            if (propInfo == null || dataSource == null || dataSource.Count == 0)
            {
                return;
            }

            var isAsc = string.Equals(sortType, SortDirectionConstants.Asc, StringComparison.OrdinalIgnoreCase);

            int Compare(T a, T b)
            {
                object valueA = isAsc
                    ? Convert.ToDecimal(propInfo.GetValue(a, null))
                    : Convert.ToDecimal(propInfo.GetValue(b, null));

                object valueB = isAsc
                    ? Convert.ToDecimal(propInfo.GetValue(b, null))
                    : Convert.ToDecimal(propInfo.GetValue(a, null));

                return ((IComparable)valueA).CompareTo(valueB);
            }

            dataSource.Sort((Comparison<T>)Compare);
        }

        public static void SortDataByProperty<T>(List<T> dataSource, string sortDirection, string sortColumnName, string sortDataType)
        {
            var propInfo = typeof(T).GetProperty(sortColumnName);

            int Compare(T a, T b)
            {
                var isAsc = string.Equals(sortDirection, SortDirectionConstants.Asc, StringComparison.OrdinalIgnoreCase);
                object valueA;
                object valueB;

                switch (sortDataType)
                {
                    case SortDataTypeConstants.StringType:
                        valueA = isAsc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
                        valueB = isAsc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);
                        break;
                    case SortDataTypeConstants.NumberType:
                        valueA = isAsc ? Convert.ToDecimal(propInfo.GetValue(a, null)) : Convert.ToDecimal(propInfo.GetValue(b, null));
                        valueB = isAsc ? Convert.ToDecimal(propInfo.GetValue(b, null)) : Convert.ToDecimal(propInfo.GetValue(a, null));
                        break;
                    case SortDataTypeConstants.DateType:
                        valueA = isAsc ? Convert.ToDateTime(propInfo.GetValue(a, null)) : Convert.ToDateTime(propInfo.GetValue(b, null));
                        valueB = isAsc ? Convert.ToDateTime(propInfo.GetValue(b, null)) : Convert.ToDateTime(propInfo.GetValue(a, null));
                        break;
                    default:
                        valueA = isAsc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
                        valueB = isAsc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);
                        break;
                }

                return valueA is IComparable comparable ? comparable.CompareTo(valueB) : 0;
            }

            dataSource.Sort((Comparison<T>)Compare);
        }
    }
}
