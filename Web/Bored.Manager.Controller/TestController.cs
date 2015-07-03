using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Bored.Model;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Common;
using Manage.ViewModel;

namespace Bored.Manager.Controllers
{
    public class TestController:Controller
    {
        private readonly IManageUsersService _manageUsersBll;
        
        public TestController(IManageUsersService manageUsersBll)
        {
            _manageUsersBll = manageUsersBll;
        }


        public ActionResult Login()
        {

            return View(CachTest.Message);
        }

        [HttpPost]
        public JsonResult Login(string userName, string passWord)
        {
            
            return Json(new { result = false, msg = "账号密码错误" });
        }

        public ActionResult htmlTest()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }
        public ActionResult list()
        {
            return View();
        }
        [ValidateInput(false)]
        public JsonResult AddR(ManageUsersDto model)
        {
            var result=_manageUsersBll.Add(model);
            return Json(new JsonDto
            {
                Error= "添加失败",
                Result=result>0
            });
        }

        [ValidateInput(false)]
        public JsonResult Edit(ManageUsersDto model)
        {
            var result = _manageUsersBll.Update(model);
            return Json(new JsonDto
            {
                Error = "修改失败",
                Result = result
            });
        }

        public JsonResult GetModel(int id)
        {
            return Json(_manageUsersBll.GetModel(id));
        }

        public JsonResult GetData(string name,int page=1,int rows=10)
        {
            int count;
            Func<ManageUsers, bool> where = null;
            if (!string.IsNullOrEmpty(name))
                where = t => t.UName.Contains(name);
            where += t => !t.IsDel;
            var cur = _manageUsersBll.GetViewPage(page, rows,"");
            return Json(cur, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

         [HttpPost]
        public JsonResult Delete(string id)
         {
             var result=_manageUsersBll.Delete(id);

             return Json(new JsonDto { Result = result, Error = "不知道什么问题" }, JsonRequestBehavior.DenyGet);
         }
    }

    


    public class PageData
    {
        public PageData()
        {
            this.PageIndex = 1;
            this.PageSize = 15;
        }
        /// <summary>
        /// 查询Data
        /// </summary>
        public object SearchData { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                try
                {
                    return DataCount % PageSize != 0 ? DataCount / PageSize + 1 : DataCount / PageSize;
                }
                catch
                {
                    return 0;
                }
            }
        }

    }

    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CachTest
    {
        public static string Message
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string message = context.Cache["message"] as string;
                if (string.IsNullOrEmpty(message))
                {
                    string path = @"D:\log.txt";
                    message = File.ReadAllText(path);
                    //context.Cache.Add("message", message, new CacheDependency(path),
                    //    Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0),
                    //    CacheItemPriority.AboveNormal, CallBack);
                    context.Cache.Insert("message",message);
                }
                return message;
            }
        }

        private static void CallBack(string key, object value, CacheItemRemovedReason reson)
        {

        }
    }
}