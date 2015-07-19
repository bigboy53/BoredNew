using System.Web.Mvc;
using DKD.Core.Config;
using DKD.Core.Config.Models;

namespace DKD.Framework
{
    /// <summary>
    /// DKD 控制器基类
    /// </summary>
    public class BaseController : Controller
    {

        #region 控制器与视力重定义

        #region 私有属性

        /// <summary>
        /// 配置缓存对像
        /// </summary>
        private FrameworkConfig CACHEOBJECT = null;

        /// <summary>
        /// 管理View路径
        /// </summary>
        private string MANAGEVIEWPATH = string.Empty;

        /// <summary>
        /// 对话框View路径
        /// </summary>
        private string DIALOGVIEWPATH = string.Empty;

        /// <summary>
        /// 普通View路径
        /// </summary>
        private string NORMALVIEWPATH = string.Empty;

        /// <summary>
        /// 前台会员View
        /// </summary>
        private string MEMBERVIEWPATH = string.Empty;

        /// <summary>
        /// 控件View路径
        /// </summary>
        private string CONTROLPATH = string.Empty;

        /// <summary>
        /// 主题路径
        /// </summary>
        private string CURRENTABSOLUTTHEMPATH = string.Empty;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            //base.ValidateRequest = false;

            CACHEOBJECT = CachedConfigContext.Current.FrameworkConfig;
            CURRENTABSOLUTTHEMPATH = CACHEOBJECT.ThemesPath + "/" + CACHEOBJECT.CurrentTheme + "/";

            MANAGEVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.ManageViewPath;
            DIALOGVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.DialogViewPath;
            NORMALVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.NormalViewPath;
            MEMBERVIEWPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.MemberViewPath;
            CONTROLPATH = CURRENTABSOLUTTHEMPATH + CACHEOBJECT.ControlPath + "/";
        }


        #endregion

        #region 后台管理View

        /// <summary>
        /// 应用管理View
        /// </summary>
        /// <returns></returns>
        public ViewResult ManageView()
        {
            return View(MANAGEVIEWPATH + _getCurrentViewPath());
        }

        /// <summary>
        /// 应用管理View
        /// </summary>
        /// <param name="model">传参数到View</param>
        /// <returns></returns>
        public ViewResult ManageView(object model)
        {
            return View(MANAGEVIEWPATH + _getCurrentViewPath(), model);
        }


        #endregion

        #region 前台普通View

        /// <summary>
        /// 普通View
        /// </summary>
        /// <returns></returns>
        public ViewResult NormalView()
        {
            return View(NORMALVIEWPATH + _getCurrentViewPath());
        }

        /// <summary>
        /// 普通View
        /// </summary>
        /// <param name="model">传参数到View</param>
        /// <returns></returns>
        public ViewResult NormalView(object model)
        {
            return View(NORMALVIEWPATH + _getCurrentViewPath(), model);
        }


        #endregion

        #region 前台会员View

        /// <summary>
        /// 前台会员View
        /// </summary>
        /// <returns></returns>
        public ViewResult MemberView()
        {
            return View(MEMBERVIEWPATH + _getCurrentViewPath());
        }

        /// <summary>
        /// 前台会员View
        /// </summary>
        /// <param name="model">传参数到View</param>
        /// <returns></returns>
        public ViewResult MemberView(object model)
        {
            return View(MEMBERVIEWPATH + _getCurrentViewPath(), model);
        }


        #endregion

        #region 对话框View

        /// <summary>
        /// 对话框View
        /// </summary>
        /// <returns></returns>
        public ViewResult DialogView()
        {
            return View(DIALOGVIEWPATH + _getCurrentViewPath());
        }

        /// <summary>
        /// 对话框View
        /// </summary>
        /// <param name="model">传参数到View</param>
        /// <returns></returns>
        public ViewResult DialogView(object model)
        {
            return View(DIALOGVIEWPATH + _getCurrentViewPath(), model);
        }

        #endregion

        #region 附助方法

        /// <summary>
        /// 获取当前Action的路径如 /controler/action.aspx
        /// </summary>
        /// <returns></returns>
        private string _getCurrentViewPath()
        {
            return string.Format("/{0}/{1}.cshtml", ControllerContext.RouteData.Values["controller"], ControllerContext.RouteData.Values["action"]);
        }

        #endregion

        #region 重定向

        /// <summary>
        /// 重定向到来源页
        /// </summary>
        /// <param name="noFoundUrl">当没有来源页里要定向的页</param>
        /// <returns></returns>
        public ActionResult RedirectReffer(object noFoundUrl)
        {
            if (Request.UrlReferrer == null)
                return Redirect(noFoundUrl.ToString());
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// 重定向到来源页
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param> 
        /// <returns></returns>
        public ActionResult RedirectReffer(string action)
        {
            if (Request.UrlReferrer == null)
                return RedirectToAction(action);
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        ///  重定向到来源页
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param> 
        /// <param name="routeValue">路由值</param> 
        /// <returns></returns>
        public ActionResult RedirectReffer(string action, object routeValue)
        {
            if (Request.UrlReferrer == null)
                return RedirectToAction(action, routeValue);
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// 重定向到来源页
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param>
        /// <param name="controller">当没有来源页里要定向的Controller名</param>
        /// <returns></returns>
        public ActionResult RedirectReffer(string action, string controller)
        {
            if (Request.UrlReferrer == null)
                return RedirectToAction(action, controller);
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// 重定向到来源页
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param>
        /// <param name="controller">当没有来源页里要定向的Controller名</param>
        /// <param name="routeValue">路由值</param> 
        /// <returns></returns>
        public ActionResult RedirectReffer(string action, string controller, object routeValue)
        {
            if (Request.UrlReferrer == null)
                return RedirectToAction(action, controller, routeValue);
            return Redirect(Request.UrlReferrer.ToString());
        }

        #endregion

        #region 重定向ToUrl

        /// <summary>
        /// 重定向到Url里ToUrl
        /// </summary>
        /// <param name="noFoundUrl">当没有来源页里要定向的页</param>
        /// <returns></returns>
        public ActionResult RedirectToUrl(object noFoundUrl)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ToUrl"]))
                return Redirect(noFoundUrl.ToString());
            return Redirect(Request.QueryString["ToUrl"].ToString());
        }

        /// <summary>
        /// 重定向到Url里ToUrl
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param> 
        /// <returns></returns>
        public ActionResult RedirectToUrl(string action)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ToUrl"]))
                return RedirectToAction(action);
            return Redirect(Request.QueryString["ToUrl"].ToString());
        }

        /// <summary>
        ///  重定向到Url里ToUrl
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param> 
        /// <param name="routeValue">路由值</param> 
        /// <returns></returns>
        public ActionResult RedirectToUrl(string action, object routeValue)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ToUrl"]))
                return RedirectToAction(action, routeValue);
            return Redirect(Request.QueryString["ToUrl"].ToString());
        }

        /// <summary>
        /// 重定向到Url里ToUrl
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param>
        /// <param name="controller">当没有来源页里要定向的Controller名</param>
        /// <returns></returns>
        public ActionResult RedirectToUrl(string action, string controller)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ToUrl"]))
                return RedirectToAction(action, controller);
            return Redirect(Request.QueryString["ToUrl"].ToString());
        }

        /// <summary>
        /// 重定向到Url里ToUrl
        /// </summary>
        /// <param name="action">当没有来源页里要定向的Action名</param>
        /// <param name="controller">当没有来源页里要定向的Controller名</param>
        /// <param name="routeValue">路由值</param> 
        /// <returns></returns>
        public ActionResult RedirectToUrl(string action, string controller, object routeValue)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ToUrl"]))
                return RedirectToAction(action, controller, routeValue);
            return Redirect(Request.QueryString["ToUrl"].ToString());
        }

        #endregion

        #region 文件下载

        /// <summary>
        /// 文件下载 ,以指定编码下载
        /// </summary>
        /// <param name="encoder">文件编码</param>
        /// <param name="content">文件内容</param>
        /// <param name="contentType">文件MiniType</param>
        /// <param name="downloadFileName">下载名</param>
        /// <returns></returns>
        public FileResult File(System.Text.Encoding encoder, string content, string contentType, string downloadFileName)
        {
            return File(encoder.GetBytes(content), contentType, downloadFileName);
        }

        /// <summary>
        /// 文件下载 ,以默认编码（System.Text.Encoding.Default）下载
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="contentType">文件MiniType</param>
        /// <param name="downloadFileName">下载名</param>
        /// <returns></returns>
        public FileResult File(string content, string contentType, string downloadFileName)
        {
            return File(System.Text.Encoding.Default, content, contentType, downloadFileName);
        }

        #endregion

        #region
        public JsonResult ReturnJson(bool result, string msg)
        {
            return Json(new JsonDto
            {
                Error = msg,
                Result = result
            });
        }

        public JsonResult JsonOk()
        {
            return Json(new JsonDto
            {
                Result = true
            });
        }
        #endregion

        #endregion

        #region 其它属性定义

        /// <summary>
        /// 是否以POST方式请求
        /// </summary>
        public bool IsPost { get { return Request.HttpMethod.ToUpper().Trim() == "POST"; } }

        /// <summary>
        /// 控件所在路径,最后带“/”
        /// </summary>
        public string ControlPath
        {
            get
            {
                return CONTROLPATH;
            }
        }

        #endregion
    }
}
