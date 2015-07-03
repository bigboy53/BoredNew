using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Web;

namespace DKD.Framework.Files
{
    public class FileManager
    {
        private static string strRootFolder;  //定义操作的根目录

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool FileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                return true;
            }

            FileIOPermission permission = null;
            try
            {
                permission = new FileIOPermission(FileIOPermissionAccess.Read, filePath);
            }
            catch
            {
                return false;
            }
            try
            {
                permission.Demand();

            }
            catch (Exception exception)
            {
                // Foe.Common.Log.ErrorFormat(string.Format("FileManager doesn't have the right to read the config file \"{0}\". Cause : {1}", filePath, exception.Message), exception);
            }
            return false;
        }

        /// <summary>
        /// 操作目录的根目录
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            return strRootFolder;
        }

        /// <summary>
        /// 写根目录
        /// </summary>
        /// <param name="path"></param>
        public static void SetRootPath(string path)
        {
            strRootFolder = path;
        }

        /// <summary>
        /// 读取根目录下的列表(包括文件夹，文件)
        /// </summary>
        /// <returns></returns>
        public static List<FileModel> GetItems()
        {
            return GetItems(strRootFolder);
        }

        /// <summary>
        /// 读取列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<FileModel> GetItems(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            List<FileModel> list = new List<FileModel>();
            foreach (string s in folders)
            {
                FileModel item = new FileModel();
                DirectoryInfo di = new DirectoryInfo(s);
                item.Name = di.Name;
                item.FullName = di.FullName;
                item.CreationDate = di.CreationTime;
                item.IsFolder = false;
                //item.Size = GetDirectoryLength(s);
                list.Add(item);
            }
            foreach (string s in files)
            {
                FileModel item = new FileModel();
                FileInfo fi = new FileInfo(s);
                item.Name = fi.Name;
                item.FullName = fi.FullName;
                item.CreationDate = fi.CreationTime;
                item.IsFolder = true;
                item.Size = fi.Length;
                list.Add(item);
            }

            if (path.Replace("\\", "").Replace("/", "").Length != strRootFolder.Replace("\\", "").Replace("/", "").Length + 36)
            {
                FileModel topitem = new FileModel();
                DirectoryInfo topdi = new DirectoryInfo(path).Parent;
                topitem.Name = "[上一级]";
                topitem.FullName = topdi.FullName;
                list.Insert(0, topitem);

                FileModel rootitem = new FileModel();
                DirectoryInfo rootdi = new DirectoryInfo(strRootFolder);
                rootitem.Name = "[根目录]";
                rootitem.FullName = rootdi.FullName;
                list.Insert(0, rootitem);

            }
            return list;
        }

        /// <summary>
        /// 获取指定路径下面的所有文件集合（不包括文件夹）
        /// </summary>
        /// <param name="path">指定路径</param>
        /// <param name="isRecursion">是否递归</param>
        /// <returns></returns>
        public static List<FileModel> GetListFileItems(string path, bool isRecursion = false)
        {
            if (!Directory.Exists(path)) return null;
            //2014-11-13 蒋俊杰修改代码，逻辑变更为递归获取当前路径下所有的文件（不包含文件夹）
            //var files = Directory.GetFiles(path);
            //return files.Select(s => new FileInfo(s)).Select(fi => new FileModel
            //{
            //    Name = fi.Name,
            //    FullName = fi.FullName,
            //    CreationDate = fi.CreationTime,
            //    IsFolder = false,
            //    Size = fi.Length
            //}).ToList();
            var flist = new List<FileModel>();
            var files = Directory.GetFiles(path);
            files.ToList().ForEach(m =>
            {
                var fi = new FileInfo(m);
                flist.Add(new FileModel
                {
                    Name = fi.Name,
                    FullName = fi.FullName,
                    CreationDate = fi.CreationTime,
                    IsFolder = false,
                    Size = fi.Length
                });
            });
            if (isRecursion) {
                var dirs = Directory.GetDirectories(path);
                if (dirs != null && dirs.Length > 0)
                {
                    dirs.ToList().ForEach(m =>
                    {
                        //flist.AddRange(GetListFileItems(m,true));
                        var tmpfiles = GetListFileItems(m, true);
                        if (tmpfiles != null && tmpfiles.Count > 0) {
                            tmpfiles.ForEach(tmp => flist.Add(tmp));
                        }
                    });
                }
            }
            return flist;
        }

        /// <summary>
        /// 递归获取指定路径下面的所有文件（不包括文件夹）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void GetAllListFileItems(string path, List<FileModel> list)
        {
            var newlist = Directory.GetFiles(path).Select(p => new FileInfo(p)).Select(fi => new FileModel
            {
                Name = fi.Name,
                FullName = fi.FullName,
                CreationDate = fi.CreationTime,
                IsFolder = false,
                Size = fi.Length
            }).ToList();
            list.AddRange(newlist);
            foreach (var s in Directory.GetDirectories(path))
            {
                GetAllListFileItems(s, list);
            }
        }

        /// <summary>
        /// 读取文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        public static void CreateFolder(string name, string parentName)
        {
            DirectoryInfo di = new DirectoryInfo(parentName);
            di.CreateSubdirectory(name);
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExistsFolder(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CreateFolder(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path"></param>
        public static bool DeleteFolder(string path)
        {
            try
            {
                foreach (string var in Directory.GetFiles(path))
                {
                    File.Delete(var);
                }

                if (Directory.GetDirectories(path).Length == 0)
                {
                    Directory.Delete(path);
                    return true;
                }

                foreach (string var in Directory.GetDirectories(path))
                {
                    DeleteFolder(var);
                }

                Directory.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static bool MoveFolder(string oldPath, string newPath)
        {
            try
            {
                Directory.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        public static bool CreateFile(string filename, string path)
        {
            try
            {
                FileStream fs = File.Create(path + "\\" + filename);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static bool CreateFile(string filename, string path, byte[] contents)
        {
            try
            {
                FileStream fs = File.Create(path + "\\" + filename);
                fs.Write(contents, 0, contents.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 上传文件并返回文件全名称
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool SaveFile(string directory, string filename, HttpPostedFile file)
        {

            directory = directory.Replace("/", "\\");
            if (directory.Substring(directory.Length - 1) != "\\")
            {
                directory = directory + "\\";
            }

            DirectoryInfo uploadPath = new DirectoryInfo(directory);
            if (!uploadPath.Exists)
            {
                uploadPath.Create();
            }

            file.SaveAs(directory + filename);

            return true;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        public static string OpenText(string parentName)
        {
            StringBuilder sbstr = new StringBuilder("");
            StreamReader sr = new StreamReader(parentName, Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                sbstr.Append(sr.ReadLine() + "\n");
            }
            return sbstr.ToString();
        }

        /// <summary>
        /// 写入一个新文件，在文件中写入内容，然后关闭文件。如果目标文件已存在，则改写该文件。 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        public static bool WriteAllText(string parentName, string contents)
        {
            try
            {
                File.WriteAllText(parentName, contents, Encoding.Unicode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static bool MoveFile(string oldPath, string newPath)
        {
            try
            {
                File.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileModel GetItemInfo(string path)
        {
            FileModel item = new FileModel();
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                item.Name = di.Name;
                item.FullName = di.FullName;
                item.CreationDate = di.CreationTime;
                item.IsFolder = true;
                item.LastAccessDate = di.LastAccessTime;
                item.LastWriteDate = di.LastWriteTime;
                item.FileCount = di.GetFiles().Length;
                item.SubFolderCount = di.GetDirectories().Length;
            }
            else
            {
                FileInfo fi = new FileInfo(path);
                item.Name = fi.Name;
                item.FullName = fi.FullName;
                item.CreationDate = fi.CreationTime;
                item.LastAccessDate = fi.LastAccessTime;
                item.LastWriteDate = fi.LastWriteTime;
                item.IsFolder = false;
                item.Size = fi.Length;
            }
            return item;
        }

        /// <summary>
        /// 复制文件夹 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static bool CopyFolder(string source, string destination)
        {
            try
            {
                String[] files;
                if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                    destination += Path.DirectorySeparatorChar;
                if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
                files = Directory.GetFileSystemEntries(source);
                foreach (string element in files)
                {
                    if (Directory.Exists(element))
                        CopyFolder(element, destination + Path.GetFileName(element));
                    else
                        File.Copy(element, destination + Path.GetFileName(element), true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文件夹的大小
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }

            //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                foreach (DirectoryInfo d in dis)
                {
                    len += GetDirectoryLength(d.FullName);
                }
            }
            return len;
        }


        /// <summary>
        /// 判断文件名是否合法
        /// </summary>
        /// <param name="str">文件名</param>
        /// <returns></returns>
        public static String IsFileName(String strExtension)
        {
            strExtension = strExtension.ToLower();//变为小写

            if (String.IsNullOrEmpty(strExtension))
            {
                return "请输入文件名!";
            }

            if (strExtension.LastIndexOf(".") > 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                return "请输入文件的后缀名!";
            }
            string[] arrExtension = { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap", ".jpg", ".gif", ".png", ".rar", ".zip", ".jpeg", ".bmp" };

            foreach (String s in arrExtension)
            {
                if (s.Equals(strExtension))
                {
                    return "";
                }

            }
            return "文件的后缀名不合法!";
        }
        /// <summary>
        ///  判断是否为不安全文件名
        /// </summary>
        /// <param name="str">文件名、文件夹名</param>
        /// <returns>bool</returns>
        public static String IsFolderName(string strExtension)
        {
            if (String.IsNullOrEmpty(strExtension))
            {
                return "请输入文件夹名";
            }
            String[] arrSpecial = new[] { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
            foreach (String special in arrSpecial)
            {
                if (strExtension.Contains(special))
                {
                    return "文件夹不能包括特殊字符!";
                }
            }
            return "";
        }

        /// <summary>
        ///  判断是否为可编辑文件
        /// </summary>
        /// <param name="str">文件名、文件夹名</param>
        /// <returns>bool</returns>
        public static bool IsCanEdit(string strExtension)
        {
            strExtension = strExtension.ToLower();//变为小写
            //得到string的.XXX的文件名后缀 LastIndexOf（得到点的位置） Substring（剪切从X的位置）

            if (strExtension.LastIndexOf(".") >= 0)
            { strExtension = strExtension.Substring(strExtension.LastIndexOf(".")); }
            else
            { strExtension = ".txt"; }//如果没有点 就当成txt文件

            //允许上传的扩展名，可以改成从配置文件中读出 
            string[] arrExtension = { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap", ".aspx", ".cs", ".php", ".jsp" };

            for (int i = 0; i < arrExtension.Length; i++)
            {
                if (strExtension.Equals(arrExtension[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 以文件流的方式下载文件
        /// </summary>
        /// <param name="fullName">文件全名称（文件路径和文件名称）</param>
        public static void DownloadFile(string fullName)
        {
            try
            {
                fullName = fullName.Replace("\\", "/");
                string filePath = fullName;
                string fileName = fullName.Substring(fullName.LastIndexOf("/"));

                //以字符流的形式下载文件 
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();

                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch
            {
                // Jscript.Alert("站点目录未找到相关文件，或没有读取权限！", false);
            }

        }

        /// <summary>
        /// 以文件流的方式下载文件
        /// </summary>
        /// <param name="fullName">文件全名称（文件路径和文件名称）</param>
        /// <param name="newFileName">新的文件名称</param>
        public static void DownloadFile(string fullName, string newFileName)
        {
            try
            {
                fullName = fullName.Replace("\\", "/");
                string filePath = fullName;

                //以字符流的形式下载文件 
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();

                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(newFileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch
            {
                //Jscript.Alert("站点目录未找到相关文件，或没有读取权限！", false);
            }

        }

        /// <summary>
        /// 以文件流的方式下载文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileName">文件名称</param>
        public static void DownloadFile(byte[] fileStream, string fileName)
        {
            try
            {
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(fileStream);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch
            {
                HttpContext.Current.Response.Close();
            }
            finally
            {
                HttpContext.Current.Response.Close();
            }
        }

        #region 解压 文件 zip 格式 rar 格式
        ///// <summary>
        /////解压文件
        ///// </summary>
        ///// <param name="fileFromUnZip">解压前的文件路径（绝对路径）</param>
        ///// <param name="fileToUnZip">解压后的文件目录（绝对路径）</param>
        //public static void UnRarAndZip(string fileFromUnZip, string fileToUnZip)
        //{
        //    using (var proce = new Process())// 开启一个进程 执行解压工作
        //    {
        //        string serverdir = SysConfigManager.UnRarExePath.Value;
        //        proce.StartInfo.UseShellExecute = false;
        //        proce.StartInfo.RedirectStandardInput = true;
        //        proce.StartInfo.RedirectStandardOutput = true;
        //        proce.StartInfo.RedirectStandardError = true;
        //        proce.StartInfo.CreateNoWindow = true;
        //        proce.StartInfo.FileName = serverdir;
        //        proce.StartInfo.Arguments = " x -inul -y " + fileFromUnZip + " " + fileToUnZip;
        //        proce.Start();//解压开始  
        //        proce.WaitForExit();
        //        proce.Close();
        //    }
        //}
        #endregion
    }
}
