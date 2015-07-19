using System;
using System.Collections.Generic;
using System.Reflection;
using DKD.Core.Config;

namespace DKD.Framework.Filter
{
    /// <summary>
    /// 反射授权Action 的附助类
    /// </summary>
    public class ActionFactory
    {
        /// <summary>
        /// 返回所有要授权的类信息
        /// </summary>
        /// <returns>Key:Contoller的名字,Value:类里所有的Action</returns>
        public static Dictionary<string, List<ActionInfoAttribute>> GetAllAction()
        {
            //action 列表
            var actionList = new Dictionary<string, List<ActionInfoAttribute>>();

            //要进行反射的DLL
            var sRefDll = CachedConfigContext.Current.FrameworkConfig.ControllerRefs.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            //遍历DLL文件
            foreach (var sDll in sRefDll)
            {
                //获得DLL里的所有类型
                var actionType = Assembly.LoadFile(System.Web.HttpContext.Current.Server.MapPath(sDll)).GetTypes();

                //遍历DLL里的Controllers
                foreach (var actionRef in actionType)
                {
                    //获取类上Controller的描述信息
                    var classObj = actionRef.GetCustomAttributes(typeof(ControllerInfoAttribute), false);

                    if (classObj.Length < 1)
                        continue;

                    var classObjAttr = classObj[0] as ControllerInfoAttribute;


                    //获得类的Action
                    var typeActionLit = new List<ActionInfoAttribute>();

                    //遍历Action Name属性
                    foreach (var mi in actionRef.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    {
                        object[] actionObj = mi.GetCustomAttributes(typeof(ActionInfoAttribute), false);

                        if (actionObj.Length < 1)
                            continue;

                        var actionRoleAttr = actionObj[0] as ActionInfoAttribute;

                        if (actionRoleAttr != null && actionRoleAttr.IsAuthorize)
                        {
                            //给Action Url赋值
                            actionRoleAttr.ActionUrl = string.Format("{0}/{1}", actionRef.Name.Replace("Controller", ""), mi.Name);

                            typeActionLit.Add(actionRoleAttr);
                        }
                    }

                    //将类信息加到返回集里
                    if (classObjAttr != null) actionList.Add(classObjAttr.Name, typeActionLit);
                }
            }

            return actionList;
        }
    }
}
