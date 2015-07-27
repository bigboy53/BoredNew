using System.Web.Mvc;
using Bored.Manager.Filter;
using Bored.IService;
using DKD.Framework;
using DKD.Framework.Filter;

namespace Bored.Manager.Controllers
{
    [ControllerInfo("用户管理")]
    public class HomeController : BaseController
    {
        private readonly IManageUsersService _manageUsersBll;

        public HomeController(IManageUsersService manageUsersBll)
        {
            _manageUsersBll = manageUsersBll;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginJ(string username,string password)
        {
            var result=_manageUsersBll.Login(username, password);

            return ReturnJson(result, result ? "" : JsonMsg.Error_Login);
        }
    }
}
