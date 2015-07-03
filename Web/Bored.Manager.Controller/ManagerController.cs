using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("管理员管理")]
    public class ManagerController : BaseController
    {
        #region  私有字段
        private readonly IManageUsersService _manageUsersBll;
        #endregion

        #region 构造函数
        public ManagerController(IManageUsersService manageUsersBll)
        {
            _manageUsersBll = manageUsersBll;
        }


        #endregion

        #region 管理员信息
        [ManageFilter("管理员", IsAuthorize = false)]
        public ActionResult ManageList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Manage_List(string name, int page = 1, int rows = 10)
        {
            var data = _manageUsersBll.GetViewPage(page, rows, name);
            return Json(data);
        }
        [ManageFilter("修改管理员", IsAuthorize = false)]
        public ActionResult ManageEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Manage_Model(int id)
        {
            return Json(_manageUsersBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult Mange_Add(ManageUsersDto model)
        {
            var isExist = _manageUsersBll.IsExist(model.UName);
            if (isExist)
                return ReturnJson(false, StringConst.Error_Exist);
            var result = _manageUsersBll.Add(model);
            return ReturnJson(result > 0, StringConst.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        public JsonResult Mange_Edit(ManageUsersDto model)
        {
            var oldModel = _manageUsersBll.GetModel(model.ID);
            if (oldModel == null ||
                (oldModel.UName != model.UName && _manageUsersBll.IsExist(model.UName)))
                return ReturnJson(false, StringConst.Error_Exist);
            var result = _manageUsersBll.Update(model);
            return ReturnJson(result, StringConst.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult Mange_Delete(string id)
        {
            var result = _manageUsersBll.Delete(id);
            return ReturnJson(result, StringConst.Error_Delete);
        }
        #endregion

    }
}