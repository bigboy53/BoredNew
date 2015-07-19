using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Bored.Service;
using Bored.Repository;
using Bored.IService;
using Bored.IRepository;
using DKD.Core.Config;
using DKD.Core.Config.Models;

namespace Bored.Manager
{
    public class RegisterAutofacFroSingle
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region Controller

            string sRefDll = CachedConfigContext.Current.FrameworkConfig.ControllerRefs;
            //var assemblies = new DirectoryInfo(
            //          HttpContext.Current.Server.MapPath("~/bin/"))
            //    .GetFiles("*.dll")
            //    .Select(r => Assembly.LoadFrom(r.FullName)).ToArray();
            builder.RegisterAssemblyTypes(Assembly.LoadFrom(HttpContext.Current.Server.MapPath(sRefDll)))
                .InstancePerHttpRequest();
            #endregion

            #region IOC注册区域
            /*注册所有的*/
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            /*注册所有的*/
            builder.RegisterType<ManageUsersService>().As<IManageUsersService>().SingleInstance();
            builder.RegisterType<ManageUsersRepository>().As<IManageUsersRepository>().SingleInstance();
            builder.RegisterType<RolesService>().As<IRolesService>().SingleInstance();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>().SingleInstance();
            builder.RegisterType<MusicService>().As<IMusicService>().SingleInstance();
            builder.RegisterType<MusicRepository>().As<IMusicRepository>().SingleInstance();
            builder.RegisterType<GameService>().As<IGameService>().SingleInstance();
            builder.RegisterType<GameRepository>().As<IGameRepository>().SingleInstance();
            builder.RegisterType<ConfigInfoService>().As<IConfigInfoService>().SingleInstance();
            builder.RegisterType<ConfigInfoRepository>().As<IConfigInfoRepository>().SingleInstance();
            builder.RegisterType<ArticleService>().As<IArticleService>().SingleInstance();
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>().SingleInstance();
            builder.RegisterType<VideoService>().As<IVideoService>().SingleInstance();
            builder.RegisterType<VideoRepository>().As<IVideoRepository>().SingleInstance();
            #endregion

            var container = builder.Build();
            CurrentContainer = container;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
        }

        public static IContainer CurrentContainer { get; set; }
    }
}