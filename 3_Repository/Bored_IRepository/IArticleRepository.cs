using System.Collections.Generic;
using Bored.Model;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IRepository
{
    public interface IArticleRepository : IDataRepository<Article>
    {
        PageData GetViewPage(int pageIndex, int pageSize, string title, string userName, int? source);

        int Add(ArticleDto entity);
        /// <summary>
        /// 获取model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleDto GetModel(int id);
    }
}
