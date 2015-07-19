using System.Web;

namespace PageHelper
{
    public class PageStruct
    {
        /// <summary>
        /// 页面代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 添加Action
        /// </summary>
        public string AddAction { get; set; }
        /// <summary>
        /// 修改Action
        /// </summary>
        public string EditAction { get; set; }
        /// <summary>
        /// 添加 修改页面路径
        /// </summary>
        public string EditPageUrl { get; set; }
        /// <summary>
        /// 展示页面
        /// </summary>
        public string ShowPageUrl { get; set; }
        /// <summary>
        /// 删除Action
        /// </summary>
        public string DeleteAction { get; set; }
        /// <summary>
        /// 获取列表Action
        /// </summary>
        public string ListAction { get; set; }
        /// <summary>
        /// 获取数据Action
        /// </summary>
        public string GetModelAction { get; set; }
        /// <summary>
        /// 当前操作的类型：add、edit
        /// </summary>
        public string OperationType
        {
            get
            {
                var optype = HttpContext.Current.Request.QueryString["optype"];
                if (!string.IsNullOrEmpty(optype))
                {
                    return optype.ToLower();
                }
                return "";
            }
        }
        public string TableId
        {
            get
            {
                return Code + "_Grid";
            }
        }

        public string FormId
        {
            get
            {
                return Code + "_Form";
            }
        }
        public string SearchBtnId
        {
            get
            {
                return Code + "_SearchBtn";
            }
        }

        /// <summary>
        /// 新增路径网页
        /// </summary>
        public string AddUrl
        {
            get
            {
                return EditPageUrl + "?optype=add";
            }
        }
        /// <summary>
        /// 修改路径网页
        /// </summary>
        public string ModifyUrl
        {
            get
            {
                return EditPageUrl + "?optype=edit";
            }
        }

        #region 构造函数

        /// <summary>
        /// 列表PageStruct
        /// </summary>
        /// <param name="code">页面唯一键</param>
        /// <param name="editPage">添加/修改页面地址</param>
        /// <param name="listAction">列表Action</param>
        /// <param name="deleteAction">删除Action</param>
        /// <returns></returns>
        public static PageStruct InitList(string code, string editPage,string listAction, string deleteAction="")
        {
            var p = new PageStruct
            {
                Code = code,
                EditPageUrl = editPage,
                DeleteAction = deleteAction,
                ListAction = listAction
            };
            return p;
        }

        /// <summary>
        /// 添加/修改PageStruct
        /// </summary>
        /// <param name="code">页面唯一键</param>
        /// <param name="addAction">添加action</param>
        /// <param name="editAction">修改action</param>
        /// <param name="getModelAction">获取数据Action</param>
        /// <returns></returns>
        public static PageStruct InitEdit(string code, string addAction, string editAction, string getModelAction)
        {
            var p = new PageStruct { Code = code, AddAction = addAction, EditAction = editAction, GetModelAction = getModelAction };
            return p;
        }

        /// <summary>
        /// 获取修改时的主键值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetModifyId(string id)
        {
            var queryId = HttpContext.Current.Request.QueryString[id];
            var formId = HttpContext.Current.Request.Form.Get(id);
            var value = string.IsNullOrEmpty(queryId) ? "0" : queryId;
            if (value == "0")
            {
                value = string.IsNullOrEmpty(formId) ? "0" : formId;
            }
            return value;
        }
        #endregion
    }
}
