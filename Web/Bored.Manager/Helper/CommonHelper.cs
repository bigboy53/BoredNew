using System.Collections.Generic;
using System.Linq;
using Autofac;
using Bored.IService;
using Bored.Manager.Core;
using DKD.Framework.Const;
using DKD.Framework.Contract.Enum;
using DKD.Framework.Filter;
using DKD.Framework.Utility;
using Manage.ViewModel;

namespace Bored.Manager.Helper
{
    public class CommonHelper
    {
        #region 私有字段

        private static readonly IRolesService RolesBll;
        private static readonly IConfigInfoService ConfigInfoBll;

        #endregion

        static CommonHelper()
        {
            RolesBll = RegisterAutofacFroSingle.CurrentContainer.Resolve<IRolesService>();
            ConfigInfoBll = RegisterAutofacFroSingle.CurrentContainer.Resolve<IConfigInfoService>();
        }

        #region 角色 权限
        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        public static List<SelectItem> RoleList()
        {
            if (RolesBll == null)
                return new List<SelectItem>();
            var data = RolesBll.GetList();
            var list = new List<SelectItem>();
            foreach (var item in data)
            {
                list.Add(new SelectItem(item.RoleName, item.ID.ToString()));
            }
            return list;
        }

        public static List<RolePermissionDto> GetRolePermission(int rid)
        {
            return RolesBll.GetPermissionList(rid);
        }

        /// <summary>
        /// 获取所有Action
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<ActionInfoAttribute>> GetPermissionList()
        {
            var list = DKD.Core.Cache.CacheManager.Cache.Get(GlobalCacheKey.PermissionList);
            if (list != null)
                return (Dictionary<string, List<ActionInfoAttribute>>)list;
            var data = ActionFactory.GetAllAction();
            DKD.Core.Cache.CacheManager.Cache.Set(GlobalCacheKey.PermissionList, data);
            return data;
        }
        #endregion
       
        public static List<SelectItem> GetConfigList()
        {
            var data = EnumHelper.GetEnumInfoList(typeof(ConfigTypeEnum.ConfigType));
            return data.Select(item => new SelectItem(item.Description, item.ID)).ToList();
        }

        public static List<SelectItem> GetConfigByType(ConfigTypeEnum.ConfigType type)
        {
            var list = ConfigInfoBll.GetAllList().Where(t => t.Type == (int)type).ToList();
            var data = new List<SelectItem>();
            foreach (var item in list)
            {
                data.Add(new SelectItem(item.Name,item.ID.ToString()));
            }
            return data;
        }
    }
}