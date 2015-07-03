using Bored.Model;
using DKD.Framework.Data;
using PageHelper;

namespace Bored.IRepository
{
    public interface IMusicRepository : IDataRepository<Music>
    {
        PageData GetViewPage(int pageIndex, int pageSize, bool? isMv, string song = "", string songer = "");
    }
}
