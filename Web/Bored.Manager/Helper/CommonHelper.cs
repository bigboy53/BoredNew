using System.Collections.Generic;
using System.Linq;
using Autofac;
using Bored.IService;
using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;
using DKD.Framework.Filter;
using Manage.ViewModel;
using CacheKey = DKD.Framework.Const.CacheKey;

namespace Bored.Manager.Helper
{
    public class CommonHelper
    {
        #region 静态属性
        private static IRolesService RolesBll
        {
            get { return RegisterAutofacFroSingle.CurrentContainer.Resolve<IRolesService>(); }
        }
        private static IConfigInfoService ConfigInfoBll
        {
            get { return RegisterAutofacFroSingle.CurrentContainer.Resolve<IConfigInfoService>(); }
        }
        #endregion

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
            var list = DKD.Core.Cache.CacheManager.Cache.Get(CacheKey.PermissionList);
            if (list != null)
                return (Dictionary<string, List<ActionInfoAttribute>>) list;
            var data = ActionFactory.GetAllAction();
            DKD.Core.Cache.CacheManager.Cache.Set(CacheKey.PermissionList, data);
            return data;
        }

        public static List<SelectItem> GetConfigList()
        {
            var data = EnumHelper.GetEnumInfoList(typeof(ConfigTypeEnum.ConfigType));
            var list = new List<SelectItem>();
            foreach (var item in data)
            {
                list.Add(new SelectItem(item.Description,item.ID));
            }
            return list;
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