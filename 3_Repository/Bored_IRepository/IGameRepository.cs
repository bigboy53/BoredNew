using Bored.Model;
using DKD.Framework.Data;
using PageHelper;

namespace Bored.IRepository
{
    public interface IGameRepository : IDataRepository<Game>
    {
        PageData GetViewPage(int page, int rows, string title);
    }
}
