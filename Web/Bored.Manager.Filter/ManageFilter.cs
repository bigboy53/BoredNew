using System.Linq;
using System.Web.Mvc;
using Autofac;
using Bored.IRepository;
using Bored.IService;
using Bored.Repository;
using Bored.Service;
using DKD.Framework.Author;
using DKD.Framework.Filter;

namespace Bored.Manager.Filter
{
    public class ManageFilter:ActionInfoAttribute
    {
        private static readonly IManageUsersService _manageUsersBll;
        private static readonly IRolesService _rolesService;


        static ManageFilter()
        {
            //看NopCommerce中Nop.Core.Infrastructure
            var builder = new ContainerBuilder();
            builder.RegisterType<ManageUsersService>().As<IManageUsersService>();
            builder.RegisterType<ManageUsersRepository>().As<IManageUsersRepository>();
            builder.RegisterType<RolesService>().As<IRolesService>();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>();
            _manageUsersBll = builder.Build().Resolve<IManageUsersService>();
            _rolesService = builder.Build().Resolve<IRolesService>();
        }

        public ManageFilter(string actionName) : base(actionName)
        {
           
        }

        public override void AuthorProcess(AuthorizationContext filterContext)
        {
            if (!AuthorHelper.IsUsing(true))
                filterContext.Result = NoAuthorizePage("Home", "login", filterContext.HttpContext.Request.Url.ToString());
            else
            {
                if (!base.IsAuthorize)
                    return;

                #region 初始变量

                var currentAction = string.Format("{0}/{1}", filterContext.RouteData.Values["controller"].ToString().Trim(), filterContext.RouteData.Values["action"].ToString().Trim()).ToUpper().Trim();
                var currenUser = AuthorHelper.GetAuthorInfo(true);
                var userModel = _manageUsersBll.GetModel((int)currenUser);
                #endregion

                #region 开始判断

                if (userModel == null)
                {
                    filterContext.Result = NoAuthorizePage("manage", "login", filterContext.HttpContext.Request.Url.ToString());
                    return;
                }
                else if (userModel.RID == 0)
                {
                    return;
                }
                else
                {
                    var actionList = _rolesService.GetPermissionList(1);

                    if (actionList.FirstOrDefault(t => t.RPUrl.Trim().ToUpper() == currentAction) == null)
                    {
                        filterContext.Result = NoAuthorizePage("manage", "NoAccess", filterContext.HttpContext.Request.UrlReferrer.ToString());
                        return;
                    }
                }

                #endregion
            }
        }
    }
}