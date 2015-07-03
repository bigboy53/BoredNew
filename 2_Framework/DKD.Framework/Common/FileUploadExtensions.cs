using System;
using System.IO;
using System.Web;
using DKD.Framework.Config;

namespace DKD.Framework.Common
{
    /// <summary>
    /// 上传文件附助类
    /// </summary>
    public static class FileUploadExtensions
    {
        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <returns></returns>
        //public static string Attachment(this HttpFileCollectionBase hfc)
        //{
        //    try
        //    {
        //        return Attachment(hfc, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        "上传文件出错".Logger(ex);
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="hfc"></param>
        ///// <param name="inputName">控件名</param>
        ///// <returns></returns>
        //public static string Attachment(this HttpFileCollectionBase hfc, string inputName)
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Request.Files.Count > 0)
        //        {
        //            HttpPostedFile file;
        //            if (string.IsNullOrEmpty(inputName))
        //                file = HttpContext.Current.Request.Files[0];
        //            else
        //                file = HttpContext.Current.Request.Files[inputName];

        //            string msg;

        //            if (file != null && file.ContentLength == 0)
        //                msg = null;
        //            else
        //            {
        //                string filename = DateTime.Now.ToString("ddHHmmssffff") + Path.GetExtension(file.FileName);

        //                string virPath = ConfigBase.Instance<FrameworkConfig>().UploadFilePath + DateTime.Now.ToString("yyyy/MM") + "/";

        //                string path = HttpContext.Current.Server.MapPath(virPath);

        //                if (!Directory.Exists(@path))
        //                    Directory.CreateDirectory(@path);

        //                file.SaveAs(path + filename);


        //                msg = virPath.Replace("~", "") + filename;

        //            }

        //            return msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        "上传文件出错".Logger(ex);
        //    }
        //    return null;
        //}


        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="hfc"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public static string Attachment(this HttpFileCollectionBase hfc, int index)
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Request.Files.Count > 0)
        //        {

        //            var file = HttpContext.Current.Request.Files[index];

        //            string msg = null;

        //            if (file.ContentLength != 0)
        //            {
        //                string filename = DateTime.Now.ToString("ddHHmmssffff") + Path.GetExtension(file.FileName);

        //                string virPath = ConfigBase.Instance<FrameworkConfig>().UploadFilePath + DateTime.Now.ToString("yyyy/MM") + "/";

        //                string path = HttpContext.Current.Server.MapPath(virPath);

        //                if (!Directory.Exists(@path))
        //                    Directory.CreateDirectory(@path);

        //                file.SaveAs(path + filename);


        //                msg = virPath.Replace("~", "") + filename;
        //            }
        //            return msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        "上传文件出错".Logger(ex);
        //    }
        //    return null;
        //}


    }
}
