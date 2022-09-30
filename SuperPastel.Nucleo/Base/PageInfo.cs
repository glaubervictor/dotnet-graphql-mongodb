namespace SuperPastel.Nucleo.Base
{
    public class PageInfo<T>
    {
        public PageInfo(long totalCount, int size, IEnumerable<T> list)
        {
            List = list;
            TotalCount = totalCount;
            PageCount = Convert.ToInt32(Math.Ceiling(totalCount / Convert.ToDouble(size)));
            Size = size;
        }

        public long TotalCount { get; private set; }
        public int Size { get; private set; }
        public int PageCount { get; private set; }
        public IEnumerable<T> List { get; private set; }

    }
}
