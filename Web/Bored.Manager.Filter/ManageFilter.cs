using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using DKD.Framework.Filter;

namespace Bored.Manager.Filter
{
    public class ManageFilter:ActionInfoAttribute
    {
        public ManageFilter(string actionName) : base(actionName) { }

        public override void AuthorProcess(AuthorizationContext filterContext)
        {
            var action = filterContext.RouteData.Values["action"].ToString().Trim();
            if (action.ToLower() == "manage_list")
            {
                //filterContext.Result = new JsonResult() { Data = new { ValidLgoin = false, Url ="/Home/Login"} };
            }
            string currentAction =
                string.Format("{0}/{1}", filterContext.RouteData.Values["controller"].ToString().Trim(),
                    action).ToUpper().Trim();
            
        }
    }
}