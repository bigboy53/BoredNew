using System.Text;
using System.Web;

namespace Bored.Manager.Core
{
    public static class ToHtmlStringHelper
    {
        public static IHtmlString AddTitle(this IHtmlString str, string tip, string description)
        {
            var strHtml = new StringBuilder(str.ToHtmlString());
            if (!string.IsNullOrEmpty(tip))
                strHtml.AppendFormat("<label>{0}</label>", tip);
            if (!string.IsNullOrEmpty(description))
                strHtml.AppendLine(string.Format("<span class=\"f_help\">{0}</span>", description));
            return new HtmlString(strHtml.ToString());
        }
    }
}