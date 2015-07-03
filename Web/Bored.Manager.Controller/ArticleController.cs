using System.Diagnostics;
using System.Threading;
using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Core.Lucene;
using DKD.Framework;
using DKD.Framework.Common;
using DKD.Framework.Const;
using DKD.Framework.Filter;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("文章管理")]
    public class ArticleController:BaseController
    {
        #region  私有字段
        private readonly IArticleService _articleBll;
        #endregion

        #region 构造函数
        public ArticleController(IArticleService musicBll)
        {
            _articleBll = musicBll;
        }
        #endregion

        #region 文章管理
        [ManageFilter("文章管理", IsAuthorize = false)]
        public ActionResult ArticleList()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchList)]
        public JsonResult Article_List(string title,string userName,int?soruce, int page = 1, int rows = 10)
        {
            var data = _articleBll.GetPage(page, rows, title, userName, soruce);
            Thread.Sleep(1000);
            return Json(data);
        }
        [ManageFilter("修改文章", IsAuthorize = false)]
        public ActionResult ArticleEdit()
        {
            return View();
        }
        [ManageFilter(PermissionConst.SearchEdit)]
        public JsonResult Article_Model(int id)
        {
            return Json(_articleBll.GetModel(id));
        }
        [ManageFilter(PermissionConst.Add)]
        [ValidateInput(false)]
        public JsonResult Article_Add(ArticleDto model)
        {
            var result = _articleBll.Add(model);
            return ReturnJson(result > 0, StringConst.Error_Add);
        }

        [ManageFilter(PermissionConst.Edit)]
        [ValidateInput(false)]
        public JsonResult Article_Edit(ArticleDto model)
        {
            var result = _articleBll.Update(model);
            return ReturnJson(result, StringConst.Error_Edit);
        }

        [ManageFilter(PermissionConst.Delete)]
        public JsonResult Article_Delete(string id)
        {
            var result = _articleBll.Delete(id);
            return ReturnJson(result, StringConst.Error_Delete);
        }
        #endregion

        public ActionResult ArticleSearch(string title)
        {
            int dataCount;
            var sw=new Stopwatch(); 
            sw.Start();
            var list = LuceneManager.Lucene.GetList(title,null, 1, 20, out dataCount);
            sw.Stop();
            double time = sw.Elapsed.TotalSeconds;//总共花费的时间
            ViewBag.Time = time;
            ViewBag.DataCount = dataCount;
            return View(list);
        }

        public FileContentResult Image()
        {
            string code;
            var ms = new System.IO.MemoryStream();
            var image = new VerifyCode().CreateImageCode(out code);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] temp = ms.GetBuffer();
            ms.Close();
            image.Dispose();
            return File(temp, "image/Jpeg");
        }

        public JsonResult ClearnLucene()
        {
            LuceneManager.Lucene.DeleteAll();
            return ReturnJson(true, "");
        }
    }
}
