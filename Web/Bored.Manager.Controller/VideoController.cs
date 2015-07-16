using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("视频管理")]
    public class VideoController:BaseController
    {
        #region  私有字段
        private readonly IVideoService _videoBll;
        #endregion

        #region 构造函数
        public VideoController(IVideoService musicBll)
        {
            _videoBll = musicBll;
        }
        #endregion

        #region 视频管理
        [ManageFilter("视频管理", IsAuthorize = false)]
        public ActionResult VideoList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Video_List(string title, int? source,int page = 1, int rows = 10)
        {
            var data = _videoBll.GetPage(page, rows, title, source);
            return Json(data);
        }
        [ManageFilter("修改视频", IsAuthorize = false)]
        public ActionResult VideoEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Video_Model(int id)
        {
            return Json(_videoBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult Video_Add(VideoDto model)
        {
            var result = _videoBll.Add(model);
            return ReturnJson(result > 0, JsonMsg.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        public JsonResult Video_Edit(VideoDto model)
        {
            var result = _videoBll.Update(model);
            return ReturnJson(result, JsonMsg.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult Video_Delete(string id)
        {
            var result = _videoBll.Delete(id);
            return ReturnJson(result, JsonMsg.Error_Delete);
        }
        #endregion
    }
}
