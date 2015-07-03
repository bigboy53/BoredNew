using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;

namespace DKD.Framework.Extensions
{

    /// <summary>
    ///Enum Label
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumLabelAttribute : Attribute
    {
        private string _label;

        /// <summary>
        /// label  that showed;
        /// </summary>
        public string Label
        {
            get
            {
                return this._label;
            }
        }

        public EnumLabelAttribute(string label)
        {
            this._label = label;
        }
    }
    public class EnumHelper
    {
        private static Dictionary<string, Dictionary<int, string>> _EnumList = new Dictionary<string, Dictionary<int, string>>(); //枚举缓存池

        /// <summary>
        /// bind source for ListControl;ex:DropDown,ListBox;
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="enumType"></param>
        public static void BindListControl(ListControl listControl, Type enumType)
        {
            listControl.Items.Clear();
            listControl.DataSource = EnumToDictionary(enumType);
            listControl.DataValueField = "key";
            listControl.DataTextField = "value";
            listControl.DataBind();
        }

        /// <summary>
        /// make sure to Convert enum to Dictionary;
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(Type enumType)
        {
            string keyName = enumType.FullName;

            if (!_EnumList.ContainsKey(keyName))
            {
                Dictionary<int, string> list = new Dictionary<int, string>();

                foreach (int i in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, i);
                    string label = name;
                    object[] atts = enumType.GetField(name).GetCustomAttributes(typeof(EnumLabelAttribute), false);
                    if (atts.Length > 0) label = ((EnumLabelAttribute)atts[0]).Label;

                    list.Add(i, label);
                }

                object obj = new object();

                if (!_EnumList.ContainsKey(keyName))
                {
                    lock (obj)
                    {
                        if (!_EnumList.ContainsKey(keyName))
                        {
                            _EnumList.Add(keyName, list);
                        }
                    }
                }
            }

            return _EnumList[keyName];
        }

        /// <summary>
        /// make sure to Convert enum to Dictionary;
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(Type enumType, bool isGetDesc)
        {
            string keyName = enumType.GUID.ToString();
            if (!_EnumList.ContainsKey(keyName))
            {
                Dictionary<int, string> list = new Dictionary<int, string>();

                foreach (int i in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, i);
                    string label = name;
                    if (isGetDesc)
                    {
                        object[] atts = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (atts.Length > 0) label = ((DescriptionAttribute)atts[0]).Description;
                    }
                    list.Add(i, label);

                }
                object obj = new object();

                if (!_EnumList.ContainsKey(keyName))
                {
                    lock (obj)
                    {
                        if (!_EnumList.ContainsKey(keyName))
                        {
                            _EnumList.Add(keyName, list);
                        }
                    }
                }
            }

            return _EnumList[keyName];
        }

        /// <summary>
        /// get the enum type's label;
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static string GetEnumLabel(Type enumType, int intValue)
        {
            var d = EnumToDictionary(enumType);
            if (d.ContainsKey(intValue))
            {
                return d[intValue];
            }
            return string.Empty;
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
        /// get value from label,ignorecase:true
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public int GetEnumValue(Type enumType, string label)
        {
            return GetEnumValue(enumType, label, true);
        }

        /// <summary>
        /// get value from label
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="label"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public int GetEnumValue(Type enumType, string label, bool ignoreCase)
        {
            var collection = EnumToDictionary(enumType);
            foreach (int item in collection.Keys)
            {
                bool b = !ignoreCase ? (collection[item] == label) : (collection[item].ToLower() == label.ToLower());
                if (b)
                {
                    return item;
                }
            }
            return -1;
        }


        /// <summary>
        /// 获取枚举属性集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumInfoList(Type enumType)
        {
            List<EnumInfo> list = new List<EnumInfo>();
            var descDic = EnumToDictionary(enumType, true);
            var labelDic = EnumToDictionary(enumType);
            foreach (var item in descDic)
            {
                EnumInfo info = new EnumInfo();
                info.ID = item.Key.ToString();
                info.Description = item.Value;
                foreach (var label in labelDic)
                {
                    if (item.Key == label.Key)
                    {
                        info.Name = label.Value;
                    }
                }
                list.Add(info);
            }
            return list;
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

        /// <summary>
        /// 通过枚举描述获取value
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static object GetValueByDescription(Type enumType, string description)
        {
            var list = GetEnumInfoList(enumType);
            object value = "";
            foreach (var item in list)
            {
                if (item.Description == description)
                {
                    value = item.ID;
                    break;
                }
            }
            return value;
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
