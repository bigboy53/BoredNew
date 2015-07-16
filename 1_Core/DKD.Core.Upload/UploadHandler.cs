using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DKD.Core.Upload
{
    public abstract class UploadHandler : IHttpHandler
    {
        protected UploadHandler()
        {
            Result = new UploadResult();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public int MaxFilesize
        {
            //10M 
            get { return 10971520; }
        }

        public virtual string[] AllowExt
        {
            get { return new[] { "txt", "rar", "zip", "jpg", "jpeg", "gif", "png", "swf" }; }
        }

        public virtual string[] ImageExt
        {
            get { return new[] { "jpg", "jpeg", "gif", "png" }; }
        }
        public UploadResult Result { get; private set; }
        public abstract string RootPath { get; }

        public abstract string GetResult(string localFileName, string uploadFilePath, string err);

        public abstract void OnUploaded(HttpContext context, string filePath);


        public void ProcessRequest(HttpContext context)
        {
            context.Response.Charset = "UTF-8";

            byte[] file;
            string fileFolder = "/" + RootPath;

            var disposition = context.Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];
            if (disposition != null)
            {
                // HTML5上传
                file = context.Request.BinaryRead(context.Request.TotalBytes);
                Result.LocalFileName = Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value;// 读取原始文件名
            }
            else
            {
                HttpFileCollection filecollection = context.Request.Files;
                HttpPostedFile postedfile = filecollection.Get(0);

                // 读取原始文件名
                Result.LocalFileName = Path.GetFileName(postedfile.FileName);

                // 初始化byte长度.
                file = new Byte[postedfile.ContentLength];

                // 转换为byte类型
                var stream = postedfile.InputStream;
                stream.Read(file, 0, postedfile.ContentLength);
                stream.Close();
            }

            var ext = Result.LocalFileName.Substring(Result.LocalFileName.LastIndexOf('.') + 1).ToLower();

            if (file.Length == 0)
                Result.Err = "无数据提交";
            else if (file.Length > MaxFilesize)
                Result.Err = "文件大小超过" + MaxFilesize + "字节";
            else if (!AllowExt.Contains(ext))
                Result.Err = "上传文件扩展名必需为：" + string.Join(",", AllowExt);
            else
            {
                fileFolder = HttpContext.Current.Server.MapPath(fileFolder);
                Result.FilePath = Path.Combine(fileFolder,
                    string.Format("{0}{1}.{2}", DateTime.Now.ToString("yyyyMMddhhmmss"), new Random(DateTime.Now.Millisecond).Next(10000), ext)
                    );
                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);

                using (var fs = new FileStream(Result.FilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(file, 0, file.Length);
                    fs.Flush();
                    fs.Close();
                }

                OnUploaded(context, Result.FilePath);
            }

            context.Response.Write(GetResult(Result.LocalFileName, Result.FilePath, Result.Err));
            context.Response.End();
        }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }

}
