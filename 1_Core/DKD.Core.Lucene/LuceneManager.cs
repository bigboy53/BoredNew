using Autofac;

namespace DKD.Core.Lucene
{
    public class LuceneManager
    {
        static LuceneManager()
         {
             var builder = new ContainerBuilder();
             builder.RegisterType<LuceneProvider>().As<ILuceneProvider>().SingleInstance();
             Lucene = builder.Build().Resolve<ILuceneProvider>();
         }
        public static ILuceneProvider Lucene { get; set; }
    }
}
