using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DKD.Core.Upload
{
    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
}
