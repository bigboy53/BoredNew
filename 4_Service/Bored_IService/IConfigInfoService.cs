using System.Collections.Generic;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IService
{
    public interface IConfigInfoService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(ConfigInfoDto model);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ConfigInfoDto GetModel(int id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        PageData GetPage(int page, int rows, int? type, string name);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(ConfigInfoDto model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);
        /// <summary>
        /// 获取所有数据(有缓存)
        /// </summary>
        /// <returns></returns>
        List<ConfigInfoDto> GetAllList();
        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        string GetConfigNames(string ids);
    }
}
