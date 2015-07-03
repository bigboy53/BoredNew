using System.Collections.Generic;
using Bored.Model;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.IRepository
{
    public interface IConfigInfoRepository : IDataRepository<ConfigInfo>
    {
        PageData GetViewPage(int pageIndex, int pageSize, int? type, string name = "");
        List<ConfigInfoDto> GetAllList();
        string GetConfigNames(string ids);
    }
}
