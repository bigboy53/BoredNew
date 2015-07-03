//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using DKD.Framework.Logger;

//namespace DKD.Framework.Database.Core
//{
//    public class DataAccessCache
//    {
//        public static Dictionary<string, Type> DynamicTypeCache = null;
//        public static Dictionary<string, object> DynamicInstanceCache = null;

//        static DataAccessCache()
//        {
//            InitDataAccessCache();
//        }


//        public static Type GetType<EntityObject>()
//        {
//            Type TypeName = typeof(EntityObject);
//            if (DynamicTypeCache.ContainsKey(TypeName.FullName))
//                return DynamicTypeCache[TypeName.FullName];
//            else
//            {
//                try
//                {
//                    //动态编译数据库问类
//                    if (!System.IO.File.Exists(Core.Helper.CompilerPathHelper.GetCompilerDataAccessPath<EntityObject>()))
//                        try
//                        {
//                            Compiler.CompilerHelper.Compiler<EntityObject>(new[] { (AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\" + Config.FrameworkConfig.Instance<Config.FrameworkConfig>().EntityDll });
//                        }
//                        catch (Exception ex)
//                        {
//                            "动态编译出错".Logger(ex);
//                        }

//                    lock (DynamicTypeCache)
//                        //反射类型加载到Cache
//                        DynamicTypeCache.Add(TypeName.FullName, Assembly.Load(DKD.Framework.Config.FrameworkConfig.Instance<DKD.Framework.Config.FrameworkConfig>().Namespace + "." + TypeName.Name.Trim()).GetTypes()[0]);

//                    lock (DynamicInstanceCache)
//                        //反射实例加载到Cache
//                        DynamicInstanceCache.Add(DynamicTypeCache[TypeName.FullName].GUID.ToString(), Activator.CreateInstance(DynamicTypeCache[TypeName.FullName]));

//                    return DynamicTypeCache[TypeName.FullName];
//                }
//                catch(Exception e) { return null; }
//            }

//        }
        
//        /// <summary>
//        /// 初始化数据访问缓存
//        /// </summary>
//        public static void InitDataAccessCache()
//        {
//            DynamicTypeCache = new Dictionary<string, Type>();
//            DynamicInstanceCache = new Dictionary<string, object>();
//        }
//    }
//}
