using System.Linq;
using Bored.Model;
using Bored.IRepository;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class MusicRepository : RepositoryBase<Music>, IMusicRepository
    {
        public PageData GetViewPage(int pageIndex, int pageSize, bool? isMv, string song = "", string songer = "")
        {
            using (var db = new BoredEntities())
            {
                var query = from m in db.Music
                    join u1 in db.MemberUser on m.UserId equals u1.ID into u2
                    from user in u2.DefaultIfEmpty()
                    select new MusicDto
                    {
                        ID = m.ID,
                        ClickCount = m.ClickCount,
                        CreateTime = m.CreateTime,
                        Image = m.Image,
                        Lyrics = m.Lyrics,
                        Song = m.Song,
                        Songer = m.Songer,
                        Url = m.Url,
                        UserId = m.UserId,
                        UserName = user.UName ?? "管理员",
                        IsMv = m.IsMv,
                        IsDel = m.IsDel
                    };


                if (isMv.HasValue)
                    query = query.Where(t => t.IsMv == isMv.Value);
                if (!string.IsNullOrEmpty(song))
                    query = query.Where(t => t.Song.Contains(song));
                if (!string.IsNullOrEmpty(songer))
                    query = query.Where(t => t.Songer.Contains(songer));
                query = query.Where(t => !t.IsDel);
                return query.OrderByDescending(t => t.CreateTime).ToPageList(pageIndex, pageSize);
            }
        }

    }
}
