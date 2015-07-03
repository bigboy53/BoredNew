using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DKD.Framework.Filter
{
    /// <summary>
    /// 控制器信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ControllerInfoAttribute : Attribute
    {
        /// <summary>
        /// 控制器描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器描述信息
        /// </summary>
        /// <param name="info">控制器描述信息</param>
        public ControllerInfoAttribute(string info)
        {
            Name = info;
        }
    }
}
