using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DKD.Framework.Filter
{
    /// <summary>
    /// 移动server http 头
    /// </summary>
    public class RemoveServerHeader : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += new EventHandler(context_PreSendRequestHeaders);
        }

        void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            try
            {
                HttpApplication app = sender as HttpApplication;
                if (null != app && null != app.Request && !app.Request.IsLocal && null != app.Context && null != app.Context.Response)
                {
                    var headers = app.Context.Response.Headers;
                    if (null != headers)
                    {
                        headers.Remove("Server");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
