using System.Linq;
using System.Web.Mvc;
using Bored.IService;
using DKD.Framework.Author;
using DKD.Framework.Filter;

namespace Bored.Manager.Filter
{
    public class ManageFilter:ActionInfoAttribute
    {
        public IManageUsersService ManageUsersBll { get; set; }
        public IRolesService RolesService { get; set; }


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
                var userModel = ManageUsersBll.GetModel((int)currenUser);
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
                    var actionList = RolesService.GetPermissionList(1);

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