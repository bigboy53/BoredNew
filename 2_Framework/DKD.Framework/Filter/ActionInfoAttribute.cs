using System;
using System.Web.Mvc;

namespace DKD.Framework.Filter
{
    /// <summary>
    /// 控制器动作(Action)描述信息
    /// </summary>
     [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class ActionInfoAttribute : FilterAttribute, IAuthorizationFilter
    {
        #region 反射用的属性

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否要进行权限的反射
        /// </summary>
        public bool IsAuthorize { get; set; }

        /// <summary>
        /// 反射出来的Action Url
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// 默认：Description=Name , IsAuthorize=true
        /// </summary>
        /// <param name="actionName">权限名</param>
        protected ActionInfoAttribute(string actionName)
        {
            Name = actionName;
            Description = actionName;
            IsAuthorize = true;
        }

        /// <summary>
        /// 默认：Description="" , IsAuthorize=false
        /// </summary>
        protected ActionInfoAttribute()
        {
            Name = "";
            Description = "";
            IsAuthorize = false;
        }


        #endregion

        /// <summary>
        /// 当没有权限时的登陆页
        /// </summary>
        /// <param name="controller">要跳转的Controller</param>
        /// <param name="action">要跳转的Action</param>
        /// <returns></returns>
        protected ActionResult NoAuthorizePage(string controller, string action)
        {
            return NoAuthorizePage(new { controller = controller, action = action, ToUrl = System.Web.HttpContext.Current.Request.Url.ToString() });
        }

        /// <summary>
        /// 当没有权限时的登陆页
        /// </summary>
        /// <param name="controller">要跳转的Controller</param>
        /// <param name="action">要跳转的Action</param>
        /// <param name="backUrl">当前请求的页面</param>
        /// <returns></returns>
        protected ActionResult NoAuthorizePage(string controller, string action, string backUrl)
        {
            return NoAuthorizePage(new { controller = controller, action = action, ToUrl = backUrl });
        }

        /// <summary>
        /// 当没有权限时的登陆页
        /// </summary>
        /// <param name="obj">要跳转的参数</param> 
        /// <returns></returns>
        protected ActionResult NoAuthorizePage(object obj)
        {
            return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(obj));
        }


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            AuthorProcess(filterContext);
        }

        /// <summary>
        /// 自定义授权过程或其它验证
        /// </summary>
        /// <param name="filterContext"></param>
        public abstract void AuthorProcess(AuthorizationContext filterContext);

    }
}