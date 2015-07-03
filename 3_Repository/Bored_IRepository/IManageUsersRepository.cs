using System;
using System.Linq.Expressions;
using Bored.Model;
using DKD.Framework.Data;
using PageHelper;

namespace Bored.IRepository
{
    public interface IManageUsersRepository : IDataRepository<ManageUsers>
    {
        PageData GetViewPage(int pageIndex, int pageSize, string name = "");
    }
}
