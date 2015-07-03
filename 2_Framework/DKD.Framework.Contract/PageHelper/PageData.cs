using System.Linq;
using EntityFramework.Extensions;

namespace PageHelper
{
    public class PageData
    {
        public PageData()
        {
            PageIndex = 1;
            PageSize = 15;
        }
        /// <summary>
        /// 查询Data
        /// </summary>
        public object SearchData { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                try
                {
                    return DataCount % PageSize != 0 ? DataCount / PageSize + 1 : DataCount / PageSize;
                }
                catch
                {
                    return 0;
                }
            }
        }

    }

    public static class PageLinqExtensions
    {
        public static PageData ToPageList<T>(this IQueryable<T> source, int pageIndex, int pageSize) where T : class
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var data = source.Skip(itemIndex).Take(pageSize).Future();
            var count = source.FutureCount();
            return new PageData
            {
                Data = data.ToList(),
                DataCount=count.Value,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
