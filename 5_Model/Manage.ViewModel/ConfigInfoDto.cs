using DKD.Framework.Contract.Enum;
using DKD.Framework.Extensions;

namespace Manage.ViewModel
{
    public class ConfigInfoDto
    {
        #region 原有属性
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        public string TypeTxt
        {
            get { return EnumHelper.GetDescFromEnum(typeof(ConfigTypeEnum.ConfigType), Type); }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        public string CreateTimeTxt
        {
            get { return CreateTime.ToString("yyyy-MM-dd"); }
        }
        public bool IsDel { get; set; }
        #endregion
    }
}
