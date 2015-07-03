using System.Collections.Generic;
using System.Text;
using System.Web;
using PageHelper;

namespace Bored.Manager.View.Helper
{
    public class GridTable:IHtmlString
    {

        #region 字段
        private string _tableName;
        private List<Column> _columnList = new List<Column>();
        private bool _isSelectAllBtn = false;
        private string _selectId;
        private string _url;
        private EditColumn _editColumn = null; //编辑列
        private EditColumn _deleteColumn = null;
        private PageStruct _pageModel = null;
        #endregion

        public GridTable(string tableName,string url)
        {
            _tableName = tableName;
            _url = url;
        }

        /// <summary>
        /// 全选按钮
        /// </summary>
        /// <param name="filed">字段</param>
        /// <returns></returns>
        public GridTable SelectAllBtn(string filed)
        {
            _selectId = filed;
            _isSelectAllBtn = true;
            return this;
        }


        #region 行
        /// <summary>
        /// 行
        /// </summary>
        /// <param name="filed">字段</param>
        /// <param name="title">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="formatter">格式化</param>
        /// <returns></returns>
        public GridTable Column(string filed, string title, int width, string formatter="")
        {
            _columnList.Add(new Column
            {
                Field=filed,
                Title= title,
                Width=width,
                Formatter=formatter
            });
            return this;
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="url">跳转地址(参数为id)</param>
        /// <param name="title">标题名称(默认为"修改")</param>
        /// <returns></returns>
        public GridTable EditColumn(string field, string url,string title="修改")
        {
            _editColumn = new EditColumn
            {
                Field = field,
                Url = url,
                Title = title
            };
            return this;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="url">跳转地址(参数为id)</param>
        /// <param name="title">标题名称(默认为"删除")</param>
        /// <returns></returns>
        public GridTable DeleteColumn(string field, string url, string title = "删除")
        {
            _deleteColumn = new EditColumn
            {
                Field = field,
                Url = url,
                Title = title
            };
            return this;
        }

        /// <summary>
        /// 行
        /// </summary>
        /// <param name="filed">字段</param>
        /// <param name="title">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="subText">截取长度</param>
        /// <param name="formatter">格式化</param>
        /// <returns></returns>
        public GridTable Column(string filed, string title, int width,int subText, string formatter="")
        {
            _columnList.Add(new Column
            {
                Field = filed,
                Title = title,
                Width = width,
                Formatter = formatter,
                SubText = subText
            });
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            var html = new StringBuilder();
            html.AppendFormat("<div id=\"{0}\" style=\"overflow-y:auto;min-height:20%;max-height:90%;\"></div>", _tableName);
            html.Append("<script>");
            html.Append("$(function () {");
            html.Append(" Ui.Table({");
            html.AppendFormat("Id: '{0}',", _tableName);
            html.AppendFormat("Url: '{0}',", _url);
            if (_isSelectAllBtn)
            {
                html.AppendFormat("CheckField:'{0}',", _selectId);
                html.AppendFormat("SelectAllId:'{0}_{1}',",_tableName ,_selectId);
            }
            html.Append("Colums: [");
            for (int i = 0; i < _columnList.Count; i++)
            {
                var col = _columnList[i];
                if (i != 0)
                {
                    html.Append(",");
                }
                html.Append("{");
                if (string.IsNullOrEmpty(col.Formatter))
                {
                    html.AppendFormat("Title: '{0}', Width: {1}, Field: '{2}'", col.Title, col.Width, col.Field);
                }
                else
                {
                    html.AppendFormat("Title: '{0}', Width: {1}, Field: '{2}', Formater:{3}", col.Title, col.Width, col.Field, col.Formatter);
                }
                if (col.SubText > 0)
                {
                    html.AppendFormat(",ShowNumbers:{0}", col.SubText);
                }
                html.Append("}");
            }
            if (_editColumn != null || _deleteColumn != null)
            {
                var action = new StringBuilder();
                if (_editColumn!=null)
                    action.Append(string.Format("<a href=\"javascript:void(0);\" onclick=\"showEdit(\\'{0}&id=\'+item.{1}+'\\',\\'修改\\')\">{2}</a>", _editColumn.Url, _editColumn.Field, _editColumn.Title));
                if (_deleteColumn != null)
                    action.Append(string.Format("&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"DeleteSingle(\\'{0}\\','+item.{1}+',\\'{3}\\')\">{2}</a>",
                        _deleteColumn.Url, _deleteColumn.Field, _deleteColumn.Title, _tableName));
                html.Append(",");
                html.Append("{");
                html.AppendFormat("Title: '操作', Width: 8, Field: '{0}', Formater:function (item, value) {2}return '{1}';{3}",
                    _editColumn == null ? _deleteColumn.Field : _editColumn.Field, action.ToString(), "{", "}");
                html.Append("}");
            }
            html.Append(" ]");
            html.Append("});");
            html.Append(" });");
            html.Append("</script>");
            var result = html.ToString();
            return result;
        }

        private string CreateHtmlString()
        {
            return ToHtmlString();
        }
    }

    /// <summary>
    /// 列
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Formatter { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 截取长度
        /// </summary>
        public int SubText { get; set; }
    }

    /// <summary>
    /// 修改列
    /// </summary>
    class EditColumn
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 操作地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Formatter { get; set; }
    }
}