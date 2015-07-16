using System;
using System.Web.Mvc;
using DKD.Framework.Logger;

namespace Bored.Manager.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExceptionFilter : FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var request = filterContext.RequestContext.HttpContext.Request;
                var message =
                        string.Format("消息类型：{0}\r\n消息内容：{1}\r\n引发异常的方法：{2}\r\n引发异常源：{3}\r\n内部错误：{4}\r\n\r\n\r\n"
                            , filterContext.Exception.GetType().Name
                            , filterContext.Exception.Message
                            , filterContext.Exception.TargetSite
                            , filterContext.Exception.Source + filterContext.Exception.StackTrace,
                            filterContext.Exception.InnerException
                            );
                if (request.IsAjaxRequest())
                {
                    //记录日志
                    LoggerHelper.Logger(message);

                    //转向
                    filterContext.ExceptionHandled = true;
                    filterContext.Result =
                        new JsonResult {Data = new {Error = message, Result = false}};
                }
                else
                {
                    //记录日志
                    LoggerHelper.Logger(message);

                    //转向
                    filterContext.ExceptionHandled = true;
                    filterContext.Result = new RedirectResult("/Home/Login");
                }
            }
        }
    }
}
