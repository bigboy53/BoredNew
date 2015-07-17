namespace DKD.Framework
{
    public class JsonDto
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 错误编号(只是标识)
        /// </summary>
        public int Code { get; set; }
    }
}
