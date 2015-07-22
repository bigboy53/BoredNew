using System.Collections.Generic;
using System.Linq;

namespace DKD.Core.Config
{
    public abstract class ConfigFileBase
    {
        public int ID { get; set; }

        /// <summary>
        /// 暂时没用到
        /// </summary>
        public virtual bool IsHasIndex
        {
            get { return false; }
        }

        internal virtual void Save() { }
    }
}
