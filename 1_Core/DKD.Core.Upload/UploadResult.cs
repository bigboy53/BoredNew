using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DKD.Core.Upload
{
    public class UploadResult
    {
        public string LocalFileName { get; set; }
        public string FilePath { get; set; }
        public string Err { get; set; }
    }
}
