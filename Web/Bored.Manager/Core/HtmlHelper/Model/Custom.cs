using System;

namespace Bored.Manager.Core
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public class Custom
    {
        /// <summary>
        /// 无验证
        /// </summary>
        public const string Empty = "";
        /// <summary>
        /// 验证手机
        /// </summary>
        public const string Phone = "custom[phone]";
        /// <summary>
        /// 验证邮箱
        /// </summary>
        public const string Email = "custom[email]";
        /// <summary>
        /// 验证整数
        /// </summary>
        public const string Integer = "custom[integer]";
        /// <summary>
        /// 验证数字
        /// </summary>
        public const string Number = "custom[number]";
        /// <summary>
        /// 验证日期
        /// </summary>
        public const string Date = "custom[date]";
        /// <summary>
        /// 验证日期格式
        /// </summary>
        public const string DateFormat = "custom[dateFormat]";
        /// <summary>
        /// 验证日期及时间格式，格式为：YYYY/MM/DD hh:mm:ss AM|PM
        /// </summary>
        public const string DateTimeFormat = "custom[dateTimeFormat]";
        /// <summary>
        /// 验证 ipv4 地址
        /// </summary>
        public const string Ipv4 = "custom[ipv4]";
        /// <summary>
        /// 验证 url 地址，需以 http://、https:// 或 ftp:// 开头
        /// </summary>
        public const string Url = "custom[url]";
        /// <summary>
        /// 只接受填数字和空格
        /// </summary>
        public const string OnlyNumberSp = "custom[onlyNumberSp]";
        /// <summary>
        /// 只接受填英文字母（大小写）和单引号(')
        /// </summary>
        public const string OnlyLetterSp = "custom[onlyLetterSp]";
        /// <summary>
        /// 只接受数字和英文字母
        /// </summary>
        public const string OnlyLetterNumber = "custom[onlyLetterNumber]";
        /// <summary>
        /// 最多选取的项目数（用于Checkbox）
        /// </summary>
        /// <returns></returns>
        public static string MaxCheckbox(int i)
        {
            return string.Format("maxCheckbox[{0}]", i);
        }
        /// <summary>
        /// 最少选取的项目数（用于Checkbox）
        /// </summary>
        /// <returns></returns>
        public static string MinCheckbox(int i)
        {
            return string.Format("minCheckbox[{0}]", i);
        }
        /// <summary>
        /// 最少输入字符数
        /// </summary>
        /// <returns></returns>
        public static string MinSize(int i)
        {
            return string.Format("minSize[{0}]", i);
        }
        /// <summary>
        /// 最多输入字符数
        /// </summary>
        /// <returns></returns>
        public static string MaxSize(int i)
        {
            return string.Format("maxSize[{0}]", i);
        }
        /// <summary>
        /// 最小值（数值的最小值）
        /// </summary>
        /// <returns></returns>
        public static string Min(int i)
        {
            return string.Format("min[{0}]", i);
        }
        /// <summary>
        /// 最大值（数值的最大值）
        /// </summary>
        /// <returns></returns>
        public static string Max(int i)
        {
            return string.Format("max[{0}]", i);
        }
        /// <summary>
        /// 日期必需在 date 或 date 的将来。格式为 YYYY/MM/DD、YYYY/M/D、YYYY-MM-DD、YYYY-M-D 或 now。
        /// </summary>
        /// <returns></returns>
        public static string Past(DateTime dt)
        {
            return string.Format("past[{0}]", dt);
        }
        /// <summary>
        /// 日期必须在 data 或 date 的过去。
        /// </summary>
        /// <returns></returns>
        public static string Future(DateTime dt)
        {
            return string.Format("future[{0}]", dt);
        }

    }
}