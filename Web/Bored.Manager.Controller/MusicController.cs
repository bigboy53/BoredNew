using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("音乐管理")]
    public class MusicController:BaseController
    {
        #region  私有字段
        private readonly IMusicService _musicBll;
        #endregion

        #region 构造函数
        public MusicController(IMusicService musicBll)
        {
            _musicBll = musicBll;
        }
        #endregion

        #region 音乐管理
        [ManageFilter("音乐管理", IsAuthorize = false)]
        public ActionResult MusicList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Music_List(string song, string songer, bool? isMv, int page = 1, int rows = 10)
        {
            var data = _musicBll.GetPage(page, rows, isMv,song,songer);
            return Json(data);
        }
        [ManageFilter("修改音乐", IsAuthorize = false)]
        public ActionResult MusicEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Music_Model(int id)
        {
            return Json(_musicBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult Music_Add(MusicDto model)
        {
            var result = _musicBll.Add(model);
            return ReturnJson(result > 0, JsonMsg.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        public JsonResult Music_Edit(MusicDto model)
        {
            var result = _musicBll.Update(model);
            return ReturnJson(result, JsonMsg.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult Music_Delete(string id)
        {
            var result = _musicBll.Delete(id);
            return ReturnJson(result, JsonMsg.Error_Delete);
        }
        #endregion


    }
}
