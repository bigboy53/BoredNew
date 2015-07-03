using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DKD.Framework.Contract;
using PageHelper;

namespace DKD.Framework.Data
{
    public interface IDataRepository<T> where T:BaseModel
    {
        #region 查询

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(Func<T, bool> where);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int GetCount(Func<T, bool> where);

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="orderType"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        PageData GetPage(int pageIndex, int pageSize, Func<T, object> order, OrderType orderType, Func<T, bool> where);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        T GetModel(Func<T, bool> exp);
        #endregion

        #region 操作

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool Insert(List<T> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="where">例子：（t => t.ID==1）</param>
        /// <param name="entity">例子：（t2 => new ManageUsers {IsDel = true}）</param>
        /// <returns></returns>
        bool Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exist(Func<T, bool> where);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> where);

        #endregion
    }
}
