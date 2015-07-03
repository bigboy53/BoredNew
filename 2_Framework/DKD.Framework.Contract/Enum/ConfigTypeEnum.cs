using System.ComponentModel;

namespace DKD.Framework.Contract.Enum
{
    public class ConfigTypeEnum
    {
        public enum ConfigType
        {
            /// <summary>
            /// 文章来源
            /// </summary>
            [Description("文章来源")]
            文章来源 = 1,
            /// <summary>
            /// 文章标签
            /// </summary>
            [Description("文章标签")]
            文章标签 = 2,
            /// <summary>
            /// 音乐来源
            /// </summary>
            [Description("音乐来源")]
            音乐来源 = 3,
            /// <summary>
            /// 音乐标签
            /// </summary>
            [Description("音乐标签")]
            音乐标签 = 4,
            /// <summary>
            /// 游戏来源
            /// </summary>
            [Description("游戏来源")]
            游戏来源 = 5,
            /// <summary>
            /// 游戏标签
            /// </summary>
            [Description("游戏标签")]
            游戏标签 = 6,
            /// <summary>
            /// 视频来源
            /// </summary>
            [Description("视频来源")]
            视频来源 = 7,
            /// <summary>
            /// 视频标签
            /// </summary>
            [Description("视频标签")]
            视频标签 = 8
        }
    }
}
