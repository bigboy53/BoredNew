using System.Collections.Generic;
using System.Linq;

namespace DKD.Core.Config
{
    public abstract class ConfigFileBase
    {
        public int ID { get; set; }

        public virtual bool IsHasIndex
        {
            get { return false; }
        }

        internal virtual void Save() { }

        internal virtual void UpdateNodeList<T>(List<T> nodeList) where T : ConfigNodeBase
        {
            foreach (var node in nodeList)
            {
                if(node.ID>0)
                    continue;
                node.ID = nodeList.Max(t => t.ID);
            }
        }
    }
}
