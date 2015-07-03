using Bored.Model;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IGameService 
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(GameDto model);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GameDto GetModel(int id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        PageData GetPage(int page, int rows,string title);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(GameDto model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);
        void UpdateLucene<T>(T model, int type) where T : Game;
    }
}
