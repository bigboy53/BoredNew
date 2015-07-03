using System.Linq;
using Bored.Model;
using Bored.IRepository;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {

        public PageData GetViewPage(int pageIndex, int pageSize, string title)
        {
            using (var db = new BoredEntities())
            {
                var query = from g in db.Game
                    join u1 in db.MemberUser on g.UserId equals u1.ID into u2
                    from user in u2.DefaultIfEmpty()
                    select new GameDto
                    {
                        ID = g.ID,
                        ClickCount = g.ClickCount,
                        CreateTime = g.CreateTime,
                        Image = g.Image,
                        Url = g.Url,
                        UserId = g.UserId,
                        UserName = user.UName ?? "管理员",
                        IsDel = g.IsDel,
                        Title = g.Title
                    };

                if (!string.IsNullOrEmpty(title))
                    query = query.Where(t => t.Title.Contains(title));
                query = query.Where(t => !t.IsDel);
                return query.OrderByDescending(t => t.CreateTime).ToPageList(pageIndex, pageSize);
            }
        }
    }
}
