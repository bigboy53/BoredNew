using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
namespace DKD.Framework.Database.Compiler
{
    public class Compiler
    {
        /// <summary>
        /// 动态编译代码到dll
        /// </summary>
        /// <param name="reference">要引用的程序集(文件形式，如：System.dll)</param>
        /// <param name="outputAssembly">程序集的输出目录</param>
        /// <param name="codeSource">要编译的代码</param>
        /// <returns></returns>
        public bool Complier(string[] reference, string outputAssembly, string codeSource)
        {
            // 创建代码编译引擎参数
            System.Collections.Generic.IDictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string>();

            dic.Add("CompilerVersion", "v4.0");

            // 创建代码编译引擎
            CSharpCodeProvider cSCP = new CSharpCodeProvider(dic);

            // 代码编译参数
            CompilerParameters cPS = new CompilerParameters();


            cPS.ReferencedAssemblies.Add("system.dll");
            cPS.ReferencedAssemblies.Add("system.data.dll");
            cPS.ReferencedAssemblies.Add("system.Xml.dll");
            cPS.ReferencedAssemblies.Add((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\DKD.Framework.dll");
            cPS.ReferencedAssemblies.Add((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\DKD.Mappings.dll");
            cPS.ReferencedAssemblies.Add((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\DKD.SearchModel.dll");
            cPS.ReferencedAssemblies.Add((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory) + @"\DKD.Querying.dll");


            if (reference != null)
                foreach (string s in reference)
                {
                    cPS.ReferencedAssemblies.Add(s);
                }

            cPS.GenerateExecutable = false;
            cPS.GenerateInMemory = false;
            cPS.OutputAssembly = outputAssembly;
            cPS.CompilerOptions = "/target:library /optimize";
            cPS.IncludeDebugInformation = false;
            

            // 代码编译结果
            CompilerResults cr = cSCP.CompileAssemblyFromSource(cPS, codeSource);


            if (cr.Errors.HasErrors)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
