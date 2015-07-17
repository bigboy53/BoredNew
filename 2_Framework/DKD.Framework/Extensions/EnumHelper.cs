using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;

namespace DKD.Framework.Extensions
{

    /// <summary>
    ///Enum Label
    /// </summary>
    public class EnumHelper
    {

        private static readonly Dictionary<string, Dictionary<int, string>> EnumList; //枚举缓存池
        static EnumHelper()
        {
            EnumList = new Dictionary<string, Dictionary<int, string>>();
        }

        /// <summary>
        /// make sure to Convert enum to Dictionary;
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isGetDesc"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(Type enumType, bool isGetDesc)
        {
            string keyName = enumType.GUID.ToString();
            if (!EnumList.ContainsKey(keyName))
            {
                var list = new Dictionary<int, string>();

                foreach (int i in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, i);
                    string label = name;
                    if (isGetDesc)
                    {
                        var atts = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (atts.Length > 0) label = ((DescriptionAttribute)atts[0]).Description;
                    }
                    list.Add(i, label);

                }
                var obj = new object();

                if (!EnumList.ContainsKey(keyName))
                {
                    lock (obj)
                    {
                        if (!EnumList.ContainsKey(keyName))
                        {
                            EnumList.Add(keyName, list);
                        }
                    }
                }
            }

            return EnumList[keyName];
        }

        /// <summary>
        /// 根据枚举类型获取描述列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumInfoList(Type enumType)
        {
            var list = new List<EnumInfo>();
            var descDic = EnumToDictionary(enumType, false);
            foreach (var item in descDic)
            {
                var info = new EnumInfo
                {
                    ID = item.Key.ToString(), 
                    Description = item.Value
                };
                list.Add(info);
            }
            return list;
        }

        /// <summary>
        /// get the enum type's description;
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Type enumType, int intValue)
        {
            var d = EnumToDictionary(enumType, true);
            if (d.ContainsKey(intValue))
            {
                return d[intValue];
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据枚举类型和枚举值（以逗号隔开的字符串）获取描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="intValue">以逗号隔开的枚举值</param>
        /// <returns>描述信息</returns>
        public static string GetEnumDescription(Type enumType, string intValue)
        {
            if (string.IsNullOrEmpty(intValue)) return "";
            var d = EnumToDictionary(enumType, true);
            string txt = string.Empty;
            string[] str = intValue.Split(',');
            txt = str.Select(s => Convert.ToInt32(s)).Where(d.ContainsKey).Aggregate(txt, (current, id) => current + (d[id] + ","));
            txt = txt.TrimEnd(',');
            return txt;
        }

        /// <summary>
        /// 通过枚举的值和类型，获取其描述信息
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescFromEnum(Type enumType, int value)
        {
            var eDesc = string.Empty;
            var eName = Enum.GetName(enumType, value);
            if (string.IsNullOrEmpty(eName))
            {
                return string.Empty;
            }
            object[] atts = enumType.GetField(eName).GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (atts.Length > 0) eDesc = ((DescriptionAttribute)atts[0]).Description;
            return eDesc;
        }

    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    public class EnumInfo
    {
        /// <summary>
        /// 枚举值
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
