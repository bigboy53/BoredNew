using System;
using System.Web;
using DKD.Framework.Common;
using Newtonsoft.Json;

namespace Bored.Manager.Handler
{
    /// <summary>
    /// ScHandler 的摘要说明
    /// </summary>
    public class ScHandler : DKD.Core.Upload.UploadHandler
    {
        public override string[] AllowExt
        {
            get { return new[] { "jpg", "jpeg", "gif", "png" }; }
        }
        
        public override string RootPath
        {
            get { return string.Format("TempFile\\{0}",DateTime.Now.ToString("yyyyMMdd")); }
        }

        public override string GetResult(string localFileName, string uploadFilePath, string err)
        {
            var result = new
            {
                msg = new
                {
                    localname = localFileName,
                    url = "/" + uploadFilePath
                        .Substring(uploadFilePath.IndexOf(RootPath, StringComparison.OrdinalIgnoreCase))
                        .Replace("\\", "/")
                },
                err = err
            };

            return JsonConvert.SerializeObject(result);
        }

        public override void OnUploaded(HttpContext context, string filePath)
        {
            //var ext = filePath.Substring(filePath.LastIndexOf('.') + 1).ToLower();
            //if (!ImageExt.Contains(ext))
            //    return;

            //if (string.IsNullOrEmpty(context.Request["thumbs"]))
            //{
            //    this.MakeThumbnail(filePath, "s", context.Request["thumbwidth"].ToInt(85), context.Request["thumbheight"].ToInt(85), string.IsNullOrEmpty(context.Request["mode"]) ? "H" : context.Request["mode"]);
            //}
            //else
            //{
            //    var thumbs = context.Request["thumbs"];
            //    foreach (var thumb in thumbs.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        var thumbparts = thumb.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //        this.MakeThumbnail(filePath, thumbparts[0], thumbparts[1].ToInt(), thumbparts[2].ToInt(), thumbparts[3]);
            //    }
            //}
        }

        private void MakeThumbnail(string filePath, string suffix, int width, int height, string mode)
        {
            string fileExt = filePath.Substring(filePath.LastIndexOf('.'));
            string fileHead = filePath.Substring(0, filePath.LastIndexOf('.'));

            var thumbPath = string.Format("{0}_{1}{2}", fileHead, suffix, fileExt); ;
            ImageUtils.MakeThumbnail(filePath, thumbPath, width, height, mode);
        }

    }
}