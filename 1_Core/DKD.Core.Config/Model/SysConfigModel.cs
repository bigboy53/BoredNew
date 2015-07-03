using System;

namespace DKD.Core.Config.Model
{
    [Serializable]
    public  class SysConfigModel
    {
        string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        string _descrption;

        public string Descrption
        {
            get { return _descrption; }
            set { _descrption = value; }
        }
        string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
