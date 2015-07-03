using System.Linq;
using Bored.Model;
using Bored.IRepository;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public PageData GetViewPage(int pageIndex, int pageSize, string title, int? source)
        {
            using (var db = new BoredEntities())
            {
                var query = from v in db.Video
                            join u1 in db.MemberUser on v.UserId equals u1.ID into u2
                    from user in u2.DefaultIfEmpty()
                    select new VideoDto
                    {
                        ID = v.ID,
                        ClickCount = v.ClickCount,
                        CreateTime = v.CreateTime,
                        Image = v.Image,
                        Url = v.Url,
                        UserId = v.UserId,
                        UserName = user.UName ?? "管理员",
                        IsDel = v.IsDel,
                        Title = v.Title,
                        Source = v.Source
                    };

                if (!string.IsNullOrEmpty(title))
                    query = query.Where(t => t.Title.Contains(title));
                query = query.Where(t => !t.IsDel);
                return query.OrderByDescending(t => t.CreateTime).ToPageList(pageIndex, pageSize);
            }
        }
    }
}
