using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("游戏管理")]
    public class GameController:BaseController
    {
        #region  私有字段
        private readonly IGameService _gameBll;
        #endregion

        #region 构造函数
        public GameController(IGameService musicBll)
        {
            _gameBll = musicBll;
        }
        #endregion

        #region 游戏管理
        [ManageFilter("游戏管理", IsAuthorize = false)]
        public ActionResult GameList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Game_List(string title, int page = 1, int rows = 10)
        {
            var data = _gameBll.GetPage(page, rows, title);
            return Json(data);
        }
        [ManageFilter("修改游戏", IsAuthorize = false)]
        public ActionResult GameEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Game_Model(int id)
        {
            return Json(_gameBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        public JsonResult Game_Add(GameDto model)
        {
            var result = _gameBll.Add(model);
            return ReturnJson(result > 0, StringConst.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        public JsonResult Game_Edit(GameDto model)
        {
            var result = _gameBll.Update(model);
            return ReturnJson(result, StringConst.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult Game_Delete(string id)
        {
            var result = _gameBll.Delete(id);
            return ReturnJson(result, StringConst.Error_Delete);
        }
        #endregion
    }
}
