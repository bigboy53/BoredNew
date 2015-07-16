using System.Collections.Generic;
using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("角色管理")]
    public class RolesController : BaseController
    {
        #region  私有字段
        private readonly IRolesService _rolesBll;
        #endregion

        #region 构造函数
        public RolesController(IRolesService rolesBll)
        {
            _rolesBll = rolesBll;
        }


        #endregion

        #region 角色管理
        [ManageFilter("角色管理",IsAuthorize = false)]
        public ActionResult RolesList()
        {
            return View();
        }
        [ManageFilter("角色管理", IsAuthorize = false)]
        public ActionResult RolesEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Roles_List(string name, int page = 1, int rows = 10)
        {
            var data = _rolesBll.GetPage(name,page,rows);
            return Json(data);
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Roles_Model(int id)
        {
            return Json(_rolesBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult Roles_Add(int? roleLock, string roleName, string actionName)
        {
            var isExist = _rolesBll.IsExist(roleName);
            if(isExist)
                return ReturnJson(false, JsonMsg.Error_Exist);
            var permission = new List<RolePermissionDto>();
            foreach (var item in actionName.Split(','))
            {
                permission.Add(new RolePermissionDto { RPName = item.Split('|')[1], RPUrl = item.Split('|')[0] });
            }
            var result =
                _rolesBll.Add(new RolesDto { RoleLock = (roleLock ?? 0) == 1, RoleName = roleName, RolePermission = permission });
            return ReturnJson(result > 0, JsonMsg.Error_Add);
        }
        [ManageFilter(PermissionConst.Edit)]
        public JsonResult Roles_Edit(int? roleLock, string roleName, string actionName,int rid)
        {
            var oldModel = _rolesBll.GetModel(rid);
            if (oldModel == null ||
                (oldModel.RoleName != roleName && _rolesBll.IsExist(roleName)))
                return ReturnJson(false, JsonMsg.Error_Exist);
            var permission = new List<RolePermissionDto>();
            foreach (var item in actionName.Split(','))
            {
                permission.Add(new RolePermissionDto { RPName = item.Split('|')[1], RPUrl = item.Split('|')[0] });
            }
            var result =
                _rolesBll.Update(new RolesDto
                {
                    ID = rid,
                    RoleLock = (roleLock ?? 0) == 1,
                    RoleName = roleName,
                    RolePermission = permission,
                    CreateTime = oldModel.CreateTime
                });
            return ReturnJson(result, JsonMsg.Error_Edit);
        }
        public JsonResult Roles_GetPremission(int rid)
        {
            return Json(_rolesBll.GetPermissionList(rid));
        }
        #endregion
    }
}