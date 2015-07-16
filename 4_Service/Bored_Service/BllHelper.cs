using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using DKD.Framework.Const;
using DKD.Framework.Files;
using DKD.Framework.Logger;

namespace Bored.Service
{
    public class BllHelper
    {
        /// <summary>
        /// 移动图片
        /// </summary>
        /// <param name="rootpath">路径</param>
        /// <param name="imageUrl"></param>
        /// <param name="oldImg">旧图片</param>
        /// <returns></returns>
        public static string RemoveImg(string rootpath,string imageUrl, string oldImg = "")
        {
            try
            {
                if (Path.HasExtension(imageUrl))
                {

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(rootpath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(rootpath));
                    var newPath = Path.GetFileName(imageUrl); //获取文件名称
                    if (FileManager.MoveFile(HttpContext.Current.Server.MapPath(imageUrl),
                        HttpContext.Current.Server.MapPath(rootpath + newPath)))
                    {
                        if (!string.IsNullOrEmpty(oldImg))
                        {
                            FileManager.DeleteFile(HttpContext.Current.Server.MapPath(oldImg));
                        }
                        return rootpath + newPath;
                    }
                    //打日志 覆盖图片失败
                    LoggerHelper.Logger(string.Format(CommonConst.Error_FileMove, "（不影响程序），图片地址：" + imageUrl,
                        "类：BllHelper.cs方法：RemoveImg "));
                    return imageUrl;
                }
                return "";
            }
            catch (Exception e)
            {
                LoggerHelper.Logger(string.Format(CommonConst.Error_FileDelete, e.Message, "类：BllHelper.cs方法：RemoveImg "));
            }
            return imageUrl;
        }
    }
}
