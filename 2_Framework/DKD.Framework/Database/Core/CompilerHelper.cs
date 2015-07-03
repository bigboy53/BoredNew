//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DKD.Framework.Database.Core.Compiler
//{
//    public class CompilerHelper
//    {
//        /// <summary>
//        /// 动态编译
//        /// </summary>
//        /// <param name="reffs">要加引的DLL文件名外来DLL请注意路径.\r\n默认引用system.dll、system.data.dll、system.Xml.dll、DKD.Database.dll</param>
//        /// <param name="dllPath">要生成的DLL路径和名字以dll加尾</param>
//        /// <param name="codeSource">要被编译的代码</param>
//        public static bool Compiler<ObjectType>(string[] reffs)
//        {
//            return new DKD.Framework.Database.Compiler.Compiler().Complier(reffs, Core.Helper.CompilerPathHelper.GetCompilerDataAccessPath<ObjectType>(), new CodeTemplate<ObjectType>().GetCodeSource());           
//        }

//    }
//}
