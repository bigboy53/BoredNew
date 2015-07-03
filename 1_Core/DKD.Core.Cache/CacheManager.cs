using Autofac;


namespace DKD.Core.Cache
{
     public class CacheManager
     {
         static CacheManager()
         {
             var builder = new ContainerBuilder();
             builder.RegisterType<LocalCacheProvider>().As<ICacheProvider>().SingleInstance();
             Cache=builder.Build().Resolve<ICacheProvider>();
         }
         public static ICacheProvider Cache { get; set; }
    }
}
