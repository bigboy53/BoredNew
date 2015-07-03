using Bored.Model;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IMusicService 
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(MusicDto model);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MusicDto GetModel(int id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="isMv"></param>
        /// <param name="song"></param>
        /// <param name="songer"></param>
        /// <returns></returns>
        PageData GetPage(int page, int rows, bool? isMv, string song = "",string songer="");
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(MusicDto model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

        void UpdateLucene<T>(T model, int type) where T : Music;
    }
}
