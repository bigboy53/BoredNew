using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Bored.Model;
using Bored.IRepository;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public PageData GetViewPage(int pageIndex, int pageSize, string title, string userName, int? source)
        {
            using (var db = new BoredEntities())
            {
                var query = from a in db.Article
                    join u1 in db.MemberUser on a.UserId equals u1.ID into u2
                    from user in u2.DefaultIfEmpty()
                    select new ArticleDto
                    {
                        Title = a.Title,
                        LookCount = a.LookCount,
                        UserId = a.UserId,
                        Source = a.Source,
                        CreatTime = a.CreateTime,
                        UserName = user.UName ?? "管理员",
                        IsDel = a.IsDel,
                        ID = a.ID
                    };
                if (!string.IsNullOrEmpty(title))
                    query = query.Where(t => t.Title.Contains(title));
                if (!string.IsNullOrEmpty(userName))
                    query = query.Where(t => t.UserName.Contains(userName));
                if (source.HasValue)
                    query = query.Where(t => t.Source == source.Value.ToString());
                query = query.Where(t => !t.IsDel);
                return query.OrderByDescending(t => t.CreatTime).ToPageList(pageIndex, pageSize);
            }
        }

        public int Add(ArticleDto model)
        {
            if (model.ArticleImages!=null)
                model.ArticleImages.ForEach(t=>t.CreateTime=DateTime.Now);
            var entity = Mapper.Map<Article>(model);
            entity.CreateTime=DateTime.Now;
            using (var db = new BoredEntities())
            {
                db.Article.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
        }

        public ArticleDto GetModel(int id)
        {
            Article data;
            using (var db = new BoredEntities())
            {
                data = db.Article.Include(t => t.ArticleImages).FirstOrDefault(t => t.ID == id);
            }
            if (data == null) 
                return new ArticleDto();
            data.ArticleImages = data.ArticleImages.OrderBy(t => t.Number).ToList();
            return Mapper.Map<ArticleDto>(data);
        }
    }
}
