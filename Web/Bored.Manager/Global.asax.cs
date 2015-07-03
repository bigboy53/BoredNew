using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DKD.Core.Lucene;
using DKD.Framework.Data;
using DKD.Framework.Logger;

namespace Bored.Manager
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            /*注册Autofac*/
            RegisterAutofacFroSingle.RegisterAutofac();
            /*注册AutoMapper*/
            RegisterAutoMapper.Excute();
            //Lucene
            LuceneManager.Lucene.StartNewThread();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //"程序错误".Logger(Server.GetLastError().GetBaseException());
        }
    }
}