using System;

namespace DKD.Core.Config
{
    public class SysConfgException:Exception
    {
        public SysConfgException(string msg)
            : base("操作失败\"" + msg + "\"请确认web.config文件是否正确配置WebPath(应用根目录地址)和SysConfigPath（系统配置文件地址）")
        {
        }
    }
}
