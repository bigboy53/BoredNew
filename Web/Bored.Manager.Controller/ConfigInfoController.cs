using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("配置管理")]
    public class ConfigInfoController:BaseController
    {
        #region  私有字段
        private readonly IConfigInfoService _configInfoBll;
        #endregion

        #region 构造函数
        public ConfigInfoController(IConfigInfoService configInfoBll)
        {
            _configInfoBll = configInfoBll;
        }
        #endregion

        #region 配置管理
        [ManageFilter("配置管理", IsAuthorize = false)]
        public ActionResult ConfigInfoList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult ConfigInfo_List(string name,int? type, int page = 1, int rows = 10)
        {
            var data = _configInfoBll.GetPage(page, rows, type, name);
            return Json(data);
        }
        [ManageFilter("配置游戏", IsAuthorize = false)]
        public ActionResult ConfigInfoEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult ConfigInfo_Model(int id)
        {
            return Json(_configInfoBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult ConfigInfo_Add(ConfigInfoDto model)
        {
            var result = _configInfoBll.Add(model);
            return ReturnJson(result > 0, JsonMsg.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        public JsonResult ConfigInfo_Edit(ConfigInfoDto model)
        {
            var result = _configInfoBll.Update(model);
            return ReturnJson(result, JsonMsg.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult ConfigInfo_Delete(string id)
        {
            var result = _configInfoBll.Delete(id);
            return ReturnJson(result, JsonMsg.Error_Delete);
        }
        #endregion
    }
}
