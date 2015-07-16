using System.Collections.Generic;

namespace DKD.Core.Lucene
{
    public interface ILuceneProvider
    {
        void Add(LuceneModel model);
        void Delete(int id, int type);
        void Edit(LuceneModel model);
        /// <summary>
        /// 启动
        /// </summary>
        void StartNewThread();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        List<LuceneModel> GetList(string keyword, int? type, int pageIndex, int pageSize, out int dataCount);

        /// <summary>
        /// 删除所有
        /// </summary>
        void DeleteAll();
    }
}
