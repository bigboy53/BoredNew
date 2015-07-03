using Bored.Model;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IArticleService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(ArticleDto model);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleDto GetModel(int id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        PageData GetPage(int page, int rows, string title, string userName, int? source);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(ArticleDto model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

        void UpdateLucene<T>(T model, int type, string image="") where T : Article;
    }
}
