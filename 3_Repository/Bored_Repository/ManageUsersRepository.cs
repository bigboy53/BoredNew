using System.Linq;
using Bored.Model;
using Bored.IRepository;
using DKD.Framework.Data;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Repository
{
    public class ManageUsersRepository : RepositoryBase<ManageUsers>, IManageUsersRepository
    {

        public PageData GetViewPage(int pageIndex, int pageSize, string name = "")
        {
            using (var db = new BoredEntities())
            {
                var query = from m in db.ManageUsers
                    join r in db.Roles on m.RID equals r.ID into rs
                    from r2 in rs.DefaultIfEmpty()
                    select new ManageUsersDto
                    {
                        ID = m.ID,
                        UName = m.UName,
                        AuthCode = m.AuthCode,
                        RID = m.RID,
                        Email = m.Email,
                        CreateTime = m.CreateTime,
                        RelName = m.RelName,
                        RoleName = r2.RoleName,
                        Tel = m.Tel,
                        IsDel = m.IsDel
                    };
                if (string.IsNullOrEmpty(name))
                    query = query.Where(t => t.IsDel == false).OrderBy(t => t.CreateTime).Skip((pageIndex - 1)*pageSize);
                else
                    query = query.Where(t => t.UName.Contains(name) && t.IsDel == false).OrderBy(t => t.CreateTime);
                return query.ToPageList(pageIndex, pageSize);
            }
        }
    }
}
