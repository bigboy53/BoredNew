using Bored.Model;
using DKD.Framework.Data;
using PageHelper;

namespace Bored.IRepository
{
    public interface IVideoRepository : IDataRepository<Video>
    {
        PageData GetViewPage(int pageIndex, int pageSize, string title, int? source);
    }
}
