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

        //
        // GET: /Home/
        [ManageFilter("首页", IsAuthorize = false)]
        public ActionResult Index()
        {
            var list = _manageUsersBll.GetList(t => t.ID > 0);
            return View();
        }

    }
}
