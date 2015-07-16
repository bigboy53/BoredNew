using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Bored.Manager.View.Helper;

namespace Bored.Manager.Helper
{
    public static class HtmlStringHelper
    {
        #region 控件
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="text">按钮名称</param>
        /// <param name="id">id/name(默认为save)</param>
        /// <param name="onclickFunction">点击函数</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString Save(string text, string id = "", string onclickFunction = "", string tip = "", string description = "")
        {
            id = id == "" ? "save" : id;
            return
                new HtmlString(
                    string.Format(
                        "<a class=\"uibutton loading\" id=\"{0}\" name=\"{0}\" title=\"{1}\" onclick=\"{2}\">{1}</a>",
                        id, text, onclickFunction)).AddTitle(tip, description);
        }

        /// <summary>
        /// phone选择框(CheckBox)
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="onText">选中名称</param>
        /// <param name="offText">未选中名称</param>
        /// <param name="isSelect">是否默认选择</param>
        /// <param name="width">宽度</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="ofun">on回调函数</param>
        /// <param name="ufun">off回调函数</param>
        /// <returns></returns>
        public static IHtmlString CheckBox(string id, string onText, string offText, bool isSelect = true,
            int width = 30, string tip = "", string description = "", string ofun = "", string ufun = "")
        {
            return
                new HtmlString(
                    string.Format(
                        " <input type=\"checkbox\" id=\"{0}\" name=\"{0}\" class=\"checkedShow\" ofun=\"{5}\" ufun=\"{6}\" cw=\"{4}px;\" value=\"1\" {1} ca=\"{2}\" uca=\"{3}\" />",
                        id, isSelect ? "checked=\"checked\"" : "", onText, offText, width,
                        string.IsNullOrEmpty(ofun) ? "" : ofun + "();", string.IsNullOrEmpty(ufun) ? "" : ufun + "();")).AddTitle(tip, description);
        }

        /// <summary>
        /// 正常CheckBox
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <param name="text">名称</param>
        /// <param name="value"></param>
        /// <param name="isSelect">是否选中(默认不选中)</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="paraClass"></param>
        /// <returns></returns>
        public static IHtmlString CheckBox(string id, string name, string text, string value, bool isSelect = false, string tip = "", string description = "", params string[] paraClass)
        {
            var classPara = new StringBuilder();
            if (paraClass.Length > 0)
            {
                foreach (var s in paraClass)
                {
                    classPara.AppendFormat(s + " ");
                }
            }
            return new HtmlString(string.Format("<input type=\"checkbox\" name=\"{4}\" id=\"{0}\" value=\"{5}\" class=\"ck {3}\" {1}/><label for=\"{0}\">{2}</label>",
                id, isSelect ? "checked=\"checked\"" : "", text, classPara, name, value)).AddTitle(tip, description);
        }

        /// <summary>
        /// Radio
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="id">id</param>
        /// <param name="val">value</param>
        /// <param name="text">text</param>
        /// <param name="isSelect">默认不选中</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString Radio(string name, string id, string val, string text, bool isSelect = false, string tip = "", string description = "")
        {
            return
                new HtmlString(
                    string.Format(
                        "<input type=\"radio\" name=\"{0}\" id=\"{4}\" value=\"{1}\" {3} class=\"ck\"/><label for=\"{4}\">{2}</label>",
                        name, val, text,
                        isSelect ? "checked=\"checked\"" : "", id)).AddTitle(tip, description);
        }

        /// <summary>
        /// Radio
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="selectItems"></param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString Radio(string id, IEnumerable<SelectItem> selectItems, string tip = "", string description = "")
        {
            var index = 0;
            var html = new StringBuilder();
            foreach (var item in selectItems)
            {
                index++;
                html.Append(Radio(id, id + index.ToString(), item.Value, item.Text, item.Selected));
            }
            return new HtmlString(html.ToString()).AddTitle(tip, description);
        }

        /// <summary>
        /// TextArea
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isValidation"></param>
        /// <param name="reg">Custom</param>
        /// <param name="tip"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static IHtmlString TextArea(string id, int width=600, int height=180, bool isValidation=true,
            string reg = Custom.Empty, string tip = "", string description = "")
        {
            return
                new HtmlString(
                    string.Format(
                        "<textarea id=\"{0}\" name=\"{0}\" style=\"width:{1}px;height:{2}px\"  class=\"validate[{3}{4}]\"></textarea>",
                        id, width, height, isValidation ? "required," : "", reg)).AddTitle(tip, description);
        }

        /// <summary>
        /// Text
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="size">类型</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString Text(string id, TextSize size, string tip = "", string description = "")
        {
            return new HtmlString(string.Format("<input type=\"text\" class=\"{1}\" id=\"{0}\" name=\"{0}\"/>", id, size)).AddTitle(tip, description);
        }

        /// <summary>
        /// Text（验证）
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="size">大小</param>
        /// <param name="validation">验证类型</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString TextKeyUp(string id, TextSize size, ValidationText validation, string tip = "", string description = "")
        {
            return
                new HtmlString(string.Format("<input type=\"text\" class=\"{1} {2}\" id=\"{0}\" name=\"{0}\"/>", id,
                    size, validation.ToString())).AddTitle(tip, description);
        }


        /// <summary>
        /// Text(字母限制长度)
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="size">大小</param>
        /// <param name="maxlength">最大长度</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString TextKeyUp(string id, TextSize size, int maxlength, string tip = "", string description = "")
        {
            return
                new HtmlString(string.Format("<input type=\"text\" class=\"{1} alphaonly\" maxlength=\"{2}\" id=\"{0}\" name=\"{0}\"/>", id,
                    size, maxlength)).AddTitle(tip, description);
        }

        /// <summary>
        /// Text(正则验证)
        /// </summary>
        /// <param name="id">id/name</param>
        /// <param name="size">大小</param>
        /// <param name="reg">正则表达式</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString TextKeyUp(string id, TextSize size, string reg, string tip = "", string description = "")
        {
            return
               new HtmlString(string.Format("<input type=\"text\" class=\"{1} regexonly\" reg=\"{2}\" id=\"{0}\" name=\"{0}\"/>", id,
                   size, reg)).AddTitle(tip, description);
        }

        /// <summary>
        /// Text
        /// </summary>
        /// <param name="id">is/name</param>
        /// <param name="size">size</param>
        /// <param name="isValidation">是否验证必填</param>
        /// <param name="reg">Custom</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="isPsw">是否为password</param>
        /// <returns></returns>
        public static IHtmlString Text(string id, TextSize size,
            string reg, bool isValidation, string tip = "", string description = "", bool isPsw = false)
        {
            return
                new HtmlString(
                    string.Format("<input type=\"{4}\"  name=\"{0}\" id=\"{0}\" class=\"validate[{2}{3}] {1}\"  />", id,
                        size.ToString(), isValidation ? "required," : "", reg, isPsw ? "password" : "text")).AddTitle(
                            tip, description);
        }
        /// <summary>
        /// Text
        /// </summary>
        /// <param name="id">is/name</param>
        /// <param name="size">size</param>
        /// <param name="isValidation">是否验证必填</param>
        /// <param name="reg">Custom</param>
        /// <returns></returns>
        public static IHtmlString Text(string id, TextSize size, bool isValidation, params string[] reg)
        {
            return Text(id, size, "", isValidation, "", reg);
        }

        /// <summary>
        /// Text
        /// </summary>
        /// <param name="id">is/name</param>
        /// <param name="size">size</param>
        /// <param name="isValidation">是否验证必填</param>
        /// <param name="description"></param>
        /// <param name="reg">Custom</param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static IHtmlString Text(string id, TextSize size, string tip, bool isValidation, string description = "", params string[] reg)
        {
            var regTxt = new StringBuilder();
            foreach (var r in reg)
            {
                regTxt.AppendFormat("{0},", r);
            }
            var regt = regTxt.ToString();
            return
                new HtmlString(
                    string.Format("<input type=\"text\"  name=\"{0}\" id=\"{0}\" class=\"validate[{2}{3}] {1}\"  />", id,
                        size.ToString(), isValidation ? "required," : "",
                        string.IsNullOrEmpty(regt) ? "" : regt.Substring(0,regt.Length - 1))).AddTitle(
                            tip, description);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="size">size</param>
        /// <param name="selectItems">data</param>
        /// <param name="validation">是否验证为空</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static IHtmlString Select(string id, TextSize size, IEnumerable<SelectItem> selectItems, bool validation = false, string tip = "", string description = "")
        {
            var str = new StringBuilder();
            str.AppendFormat("<select class=\"{1}\" id=\"{0}\"  class=\"validate[{2}]\"  name=\"{0}\">", id, size.ToString(), validation ? " required" : "");
            str.Append("<option value=\"\">请选择</option>");
            foreach (var item in selectItems)
            {
                str.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", item.Value, item.Text,
                    item.Selected ? "selected=\"selected\"" : "");
            }
            str.Append("</select>");
            return new HtmlString(str.ToString()).AddTitle(tip, description);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="selectItems">data</param>
        /// <param name="validation">是否验证为空</param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="isTag">true:tag,false:search</param>
        /// <returns></returns>
        public static IHtmlString Select(string id, IEnumerable<SelectItem> selectItems, bool validation = false, string tip = "", string description = "", bool isTag = false)
        {
            var str = new StringBuilder();
            str.AppendFormat("<select class=\"chzn-select{2}\" id=\"{0}\" {1} name=\"{0}\">", id, isTag ? "multiple" : "", validation ? " validate[required]" : "");
            if (!isTag)
                str.Append("<option value=\"\">请选择</option>");
            foreach (var item in selectItems)
            {
                str.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", item.Value, item.Text,
                    item.Selected ? "selected=\"selected\"" : "");
            }
            str.Append("</select>");
            return new HtmlString(str.ToString()).AddTitle(tip, description);
        }

        public static IHtmlString SearchButton(string id, string tableId)
        {
            var html = new StringBuilder();
            html.AppendFormat(
                "<a id=\"{0}\" class=\"btn-search\" href=\"javascript:void(0);\"></a>", id);
            html.Append("<script>");
            html.Append("$(function () {");
            html.AppendFormat("Ui.SearchBtn('{0}', '{1}');", id, tableId);
            html.Append("});");
            html.Append("</script>");
            return new HtmlString(html.ToString());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IHtmlString SaveBtn(string id, string text)
        {
            return new HtmlString(string.Format("<a class=\"uibutton loading\" id=\"{0}\" name=\"{0}\" title=\"{1}\" rel=\"1\" >{1}</a>", id, text));
        }

        /// <summary>
        /// 红色按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IHtmlString RedBtn(string id, string text)
        {
            return new HtmlString(string.Format("<a class=\"uibutton special\" id=\"{0}\" name=\"{0}\" title=\"{1}\" rel=\"1\" >{1}</a>", id, text));
        }

        /// <summary>
        /// 蓝色按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IHtmlString BlueBtn(string id, string text)
        {
            return new HtmlString(string.Format("<a class=\"uibutton loading confirm\" id=\"{0}\" name=\"{0}\" title=\"{1}\" rel=\"1\" >{1}</a>", id, text));
        }

        /// <summary>
        /// 时间选择框
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IHtmlString DateTimeText(string id)
        {
            return new HtmlString(string.Format("<input type=\"text\" id=\"{0}\" class=\" datetime  small \" name=\"{0}\" />", id));
        }

        /// <summary>
        /// 星星
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tip">提示</param>
        /// <param name="description">描述</param>
        /// <param name="defultNumber">默认选取几个星</param>
        /// <returns></returns>
        public static IHtmlString StarSelect(string id, string tip = "", string description = "", string defultNumber = "1")
        {
            return
                new HtmlString(
                    string.Format(
                        "<input name=\"{0}\" value=\"{1}\" class=\"rating_star\" id=\"{0}\" type=\"hidden\">", id,
                        defultNumber)).AddTitle(tip, description);
        }

        /// <summary>
        /// 编译器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="initFun"></param>
        /// <returns></returns>
        public static IHtmlString UeditorText(string id, int width = 300, string initFun = "")
        {
            var html =
                string.Format(
                    "<textarea ud=\"true\" id=\"{0}\" name=\"{0}\" style=\"width={1}px;heigth=200px;\"></textarea><script type=\"text/javascript\">{2}</script>",
                    id, width, initFun);
            return new HtmlString(html);
        }

        /// <summary>
        /// 编译器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="module">上传图片的Module（Article、Game...）</param>
        /// <returns></returns>
        public static IHtmlString UeditorText(string id, string module, int width = 300)
        {
            var html =
                string.Format(
                    "<textarea ud=\"true\" id=\"{0}\" name=\"{0}\" style=\"width={1}px\"></textarea><script type=\"text/javascript\"> var ue = UE.getEditor('{0}',{2}uploadModule:'{4}'{3}); </script>",
                    id, width, "{", "}",module);
            return new HtmlString(html);
        }

        public static IHtmlString HiddenText(string id)
        {
            return new HtmlString(string.Format("<input type=\"hidden\" name=\"{0}\" id=\"{0}\"/>", id));
        }

        public static IHtmlString Label(string title, string des = "")
        {
            //<label>授权码<small>输入授权码</small></label>
            if (string.IsNullOrEmpty(des))
                return new HtmlString(string.Format("<label>{0}</label>", title));
            return new HtmlString(string.Format("<label>{0}<small>{1}</small></label>", title, des));
        }
        #endregion

        #region Table操作

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="isShowClear">是否显示重填按钮</param>
        /// <param name="title">按钮名称</param>
        /// <returns></returns>
        public static IHtmlString SubmitButton(string formId, bool isShowClear = false, string title = "提交")
        {
            var str = new StringBuilder(string.Format("<a class=\"uibutton submit_form\" onclick=\"Submit('" + formId + "')\" type=\"submit\">{0}</a>", title));
            if (isShowClear)
            {
                str.Append("<a class=\"uibutton special\" onclick=\"ResetForm()\" title=\"Reset  Form\">重填</a>");
            }
            return new HtmlString(str.ToString());
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="url">请求地址或标签ID(要带"#"号)</param>
        /// <param name="isUrl">是否为请求Url(默认为是)</param>
        /// <param name="tip">标题(默认为"添加")</param>
        /// <returns></returns>
        public static IHtmlString AddButton(string url, bool isUrl = true, string tip = "添加")
        {
            return new HtmlString(string.Format("<span class=\"tip\"><a class=\"uibutton icon add\" id=\"common_add\" isUrl=\"{2}\" name=\"{0}\" title=\"{1}\">{1}</a></span>", url, tip, isUrl));
        }

        /// <summary>
        /// 返回列表按钮
        /// </summary>
        /// <param name="id">Form表单的ID</param>
        /// <param name="title">名称(默认为返回列表)</param>
        /// <param name="tab">返回列表时的tabid</param>
        /// <returns></returns>
        public static IHtmlString ReturnListButton(string id, string title = "返回列表", string tab = "")
        {
            return new HtmlString(string.Format("<span class=\"tip\"><a class=\"uibutton icon prev\" id=\"on_prev_pro\" name=\"#{1}\" onclick=\"PrevList('{0}')\">{2}</a></span>", id, tab, title));
        }

        /// <summary>
        /// 重置Form
        /// </summary>
        /// <param name="title">按钮名称(默认为"重置")</param>
        /// <returns></returns>
        public static IHtmlString ResetForm(string title = "重置")
        {
            return new HtmlString(string.Format("<span class=\"tip\"><a class=\"uibutton special\" onclick=\"ResetForm()\" title=\"{0}\">{0}</a></span>", title));
        }

        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static GridTable GridTable(string tableName, string url)
        {
            var table = new GridTable(tableName, url);
            return table;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="tableName">TableID</param>
        /// <param name="title">名称(默认为"删除")</param>
        /// <returns></returns>
        public static IHtmlString DeleteButton(string url, string tableName, string title = "删除")
        {
            return new HtmlString(string.Format("<a class=\"uibutton special DeleteAll\" table=\"{2}\" url=\"{0}\">{1}</a>", url, title, tableName));
        }

        /// <summary>
        /// 添加/修改页面初始化
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="operationType">add or edit</param>
        /// <param name="url">修改页面获取Model地址</param>
        /// <param name="idValue">修改ID</param>
        /// <param name="addUrl">添加Post地址</param>
        /// <param name="editUrl">修改Post地址</param>
        /// <param name="otherFunc">初次加载操作</param>
        /// <param name="paraFunc">参数方法</param>
        /// <returns></returns>
        public static IHtmlString InitControlSpeciality(string formId, string operationType, string url, string idValue, string addUrl, string editUrl, string otherFunc = null,string paraFunc=null)
        {
            var isAdd = operationType == "add";
            return
                new HtmlString("<script>$(function () {Ui.InitForm('" + formId + "','" + operationType + "','" + url +
                               "','" + idValue + "','" + addUrl + "','" + editUrl + "',function(){}," +
                               (string.IsNullOrEmpty(otherFunc) ? "function(){}" : otherFunc) + ");$(\"form#" + formId + "\").validationEngine(\"attach\", { onValidationComplete: function (form, status) { if (status) {Common.loading(\"正在加载...\",1); var para = " + (string.IsNullOrEmpty(paraFunc) ? "Ui.FormData(\"" + formId + "\")" : paraFunc) + "; $.ajax({ url: \"" + (isAdd ? addUrl : editUrl) + "\", type: \"post\", data: para, dataType: \"json\", success: function (data) { Common.unloading();if (data.Result) {" + (isAdd ? "Common.showSuccess('添加成功！');" : "Common.showSuccess('修改成功！');") + "$(\"form#" + formId + "\").parent().find('#on_prev_pro').click(); } else { Common.showWarning(data.Error); } }, error: function () {Common.unloading(); Common.showError('网络错误，请刷新重试！'); } }); } } });});</script>");

        }
        #endregion

        #region 上传图片
        public static IHtmlString FileUpload(string id)
        {
            var html = new StringBuilder();
            html.AppendFormat("<div id='{0}_Div' style='background: #fafafa;'>", id);
            html.Append("<div>");
            html.Append(" <div style=\"border:#fafafa 20px solid;\">");
            html.AppendFormat("<div id=\"{0}_custom\" class=\"custom-queue\"></div>", id);
            html.Append("</div>");
            html.Append("</div>");
            html.Append("<div id=\"uploadButton\">");
            html.Append("<div id=\"uploadify\"></div></div><div id=\"uploadButtondisable\"></div>");
            html.AppendFormat(
                "<div class=\"upload-group\"> <a class=\"uibutton icon add \">上传</a> <span class=\"status-message\"></span> <input type=\"hidden\" id=\"{0}\" name=\"{0}\"/> </div>", id);
            html.Append("<div class=\"imgDiv\" style='margin-left: 20px;'> </div></div>");
            html.Append("<script type=\"text/javascript\">");
            html.Append(" $(function () {");
            html.AppendFormat("$('#{0}_Div #uploadify').uploadify(", id);
            html.Append("{'uploader': '/Content/components/uploadify/uploadify.swf',");
            html.Append("'script': '/Handler/UpLoadHandler.ashx',");
            html.Append("'cancelImg': '/Content/components/uploadify/cancel.png',");
            html.Append("'multi': false,");
            html.Append("'auto': true,");
            html.Append("'fileExt': '*.jpg;*.gif;*.png;*.jpeg',");
            html.Append("'fileDesc': '图片 (.JPG, .GIF, .PNG)',");
            html.AppendFormat("'queueID': '{0}_custom',", id);
            html.Append("'wmode': 'transparent',");
            html.Append("'hideButton': true,");
            html.Append("'width': 92,");
            html.Append("'height': 26,");
            html.Append("'simUploadLimit':1,");
            html.Append("'onClearQueue': function (event) {");
            html.Append(" $('#" + id + "_Div .status-message').html(' ');},");
            html.Append("'onSelectOnce': function (event, data) {");
            html.Append(" $('#" + id + "_Div .status-message').html('准备上传...');},");
            html.Append("'onComplete': function (event, queueId, fileObj, response, data) {");
            html.Append(" var obj = $.parseJSON(response);");
            html.AppendFormat("$('#{0}').val(obj.msg.url);", id);
            html.AppendFormat("var img = $('#{0}_Div .imgShow');", id);
            html.Append("if (img&&img.length>0) {");
            html.Append("img.attr('src', obj.msg.url);");
            html.Append("} else {");
            html.Append("$('#" + id + "_Div .imgDiv').append('<img class=\"imgShow\" src=\"' + obj.msg.url + '\" width=\"100\" height=\"100\"/>');");
            html.Append("}},");
            html.Append(" 'onAllComplete': function (event, data) {");
            html.Append("if (data.errors) {");
            html.AppendFormat("$('#{0}_Div .status-message').html('上传图片' + data.filesUploaded + '错误 ：<font color=red> ' + data.errors + '</font>.');", id);
            html.Append("showError('上传图片' + data.filesUploaded + '错误 ：<font color=red> ' + data.errors + '</font>.', 2000);");
            html.Append("} else {");
            html.AppendFormat("$('#{0}_Div .status-message').html('成功上传 ' + data.filesUploaded + ' 个图片');", id);
            html.Append("showSuccess('上传图片 ' + data.filesUploaded + ' 成功', 2000);");
            html.Append("}}});});</script>");
            return new HtmlString(html.ToString());
        }

        public static IHtmlString FileUpload(string id, int uploadLimit)
        {
            var html = new StringBuilder();
            html.AppendFormat("<div id='{0}_Div' style='background: #fafafa;'>", id);
            html.Append("<div>");
            html.Append(" <div style=\"border:#fafafa 20px solid;\">");
            html.AppendFormat("<div id=\"{0}_custom\" class=\"custom-queue\"></div>", id);
            html.Append("</div>");
            html.Append("</div>");
            html.Append("<div id=\"uploadButton\">");
            html.Append("<div id=\"uploadify\"></div></div><div id=\"uploadButtondisable\"></div>");
            html.AppendFormat(
                "<div class=\"upload-group\"> <a class=\"uibutton icon add \">上传</a> <span class=\"status-message\"></span></div>", id);
            html.AppendFormat("<div class=\"albumpics\"><ul id=\"{0}sortable\" class=\"paneUl\"></ul></div></div>", id);
            html.Append("<script type=\"text/javascript\">");
            html.Append(" $(function () {");
            html.AppendFormat("$('#{0}_Div #uploadify').uploadify(", id);
            html.Append("{'uploader': '/Content/components/uploadify/uploadify.swf',");
            html.Append("'script': '/Handler/ScHandler.ashx',");
            html.Append("'cancelImg': '/Content/components/uploadify/cancel.png',");
            html.Append("'multi': true,");
            html.AppendFormat("'queueSizeLimit': {0},",uploadLimit);
            html.Append("'auto': true,");
            html.Append("'fileExt': '*.jpg;*.gif;*.png;*.jpeg',");
            html.Append("'fileDesc': '图片 (.JPG, .GIF, .PNG)',");
            html.AppendFormat("'queueID': '{0}_custom',", id);
            html.Append("'wmode': 'transparent',");
            html.Append("'hideButton': true,");
            html.Append("'width': 92,");
            html.Append("'height': 26,");
            html.AppendFormat("'simUploadLimit':{0},",uploadLimit);
            html.Append("'onClearQueue': function (event) {");
            html.Append(" $('#" + id + "_Div .status-message').html(' ');},");
            html.Append("'onSelectOnce': function (event, data) {");
            html.Append(" $('#" + id + "_Div .status-message').html('准备上传...');},");
            html.Append("'onComplete': function (event, queueId, fileObj, response, data) {");
            html.Append(" var obj = $.parseJSON(response);");
            //html.AppendFormat("$('#{0}').val(obj.msg.url);", id);
            html.AppendFormat("var img = $('#{0}_Div .imgShow');", id);
            html.Append("if (img&&img.length>0) {");
            html.Append("img.attr('src', obj.msg.url);");
            html.Append("} else {");
            html.Append(
                " var html = '<li class=\"albumImage\"><input type=\"hidden\" value=\"'+obj.msg.url+'\" name=\""+id+"\"/><div class=\"picHolder\"><span class=\"image_highlight\"></span><a href=\"' + obj.msg.url + '\" rel=\"glr\"></a><img src=\"' + obj.msg.url + '\" title=\"拖拽换位置\" width=\"160\" height=\"120\"/> <div class=\"picTitle\">' + fileObj.name + '</div><div class=\"picDel\" onclick=\"picDel(this)\"><img src=\"/Content/images/icon/color_18/trash_can.png\" /></div><ul class=\"dataImg\"><li>' + obj.msg.url + '</li><li>' + fileObj.name + '</li></ul> </div> </li>';");
            html.AppendFormat(" $(\"#{0}sortable\").append(html);ImageInit(\"{0}sortable\");", id);
            html.Append("}},");
            html.Append(" 'onAllComplete': function (event, data) {");
            html.Append("if (data.errors) {");
            html.AppendFormat("$('#{0}_Div .status-message').html('上传图片' + data.filesUploaded + '错误 ：<font color=red> ' + data.errors + '</font>.');", id);
            html.Append("showError('上传图片' + data.filesUploaded + '错误 ：<font color=red> ' + data.errors + '</font>.', 2000);");
            html.Append("} else {");
            html.AppendFormat("$('#{0}_Div .status-message').html('成功上传 ' + data.filesUploaded + ' 个图片');", id);
            html.Append("showSuccess('上传图片 ' + data.filesUploaded + ' 成功', 2000);");
            html.Append("}}});$(\"#" + id + "sortable\").sortable({ opacity: 0.6, revert: true, cursor: \"move\", zIndex: 9000 });});");
            //html.Append("function ImageInit() {");
            //html.Append("$(\"#" + id + "sortable\").find('.albumImage').unbind(\"dblclick\").bind(\"dblclick\",function() {");
            //html.Append("$(this).find(\"a[rel=glr]\").fancybox({ 'showCloseButton': true, 'centerOnScroll': true, 'overlayOpacity': 0.8, 'padding': 0 });");
            //html.Append("$(\"#" + id + "sortable\").find('a').trigger('click');");
            //html.Append("});");
            //html.Append("$(\"#" + id + "sortable\").find('.picHolder').unbind(\"hover\").unbind(\"mouseout\").bind(\"hover\", function () {");
            //html.Append("$(this).find('.picTitle').fadeTo(200, 1);");
            //html.Append("}).bind(\"mouseout\",function() {");
            //html.Append("$(this).find('.picTitle').fadeTo(200, 0);");
            //html.Append("});}");
            html.Append("function picDel(obj) {");
            html.Append("$(obj).parent().parent().remove();");
            html.Append("}");
            html.Append("</script>");
            return new HtmlString(html.ToString());
        }

        #endregion
    }

    public class SelectItem
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public SelectItem() { }
        public SelectItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public SelectItem(string text, string value, bool selected)
        {
            Text = text;
            Value = value;
            Selected = selected;
        }
    }

    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidationText
    {
        /// <summary>
        /// 只能输入数字
        /// </summary>
        numericonly,
        /// <summary>
        /// 只能为字母
        /// </summary>
        textonly,
        /// <summary>
        /// 大写
        /// </summary>
        alluppercase
    }
    public enum TextSize
    {
        full,
        large,
        medium,
        small,
        xsmall,
        xxsmall
    }

    public class  Custom
    {
        /// <summary>
        /// 无验证
        /// </summary>
        public const string Empty = "";
        /// <summary>
        /// 验证手机
        /// </summary>
        public const string Phone = "custom[phone]";
        /// <summary>
        /// 验证邮箱
        /// </summary>
        public const string Email = "custom[email]";
        /// <summary>
        /// 验证整数
        /// </summary>
        public const string Integer = "custom[integer]";
        /// <summary>
        /// 验证数字
        /// </summary>
        public const string Number = "custom[number]";
        /// <summary>
        /// 验证日期
        /// </summary>
        public const string Date = "custom[date]";
        /// <summary>
        /// 验证日期格式
        /// </summary>
        public const string DateFormat = "custom[dateFormat]";
        /// <summary>
        /// 验证日期及时间格式，格式为：YYYY/MM/DD hh:mm:ss AM|PM
        /// </summary>
        public const string DateTimeFormat = "custom[dateTimeFormat]";
        /// <summary>
        /// 验证 ipv4 地址
        /// </summary>
        public const string Ipv4 = "custom[ipv4]";
        /// <summary>
        /// 验证 url 地址，需以 http://、https:// 或 ftp:// 开头
        /// </summary>
        public const string Url = "custom[url]";
        /// <summary>
        /// 只接受填数字和空格
        /// </summary>
        public const string OnlyNumberSp = "custom[onlyNumberSp]";
        /// <summary>
        /// 只接受填英文字母（大小写）和单引号(')
        /// </summary>
        public const string OnlyLetterSp = "custom[onlyLetterSp]";
        /// <summary>
        /// 只接受数字和英文字母
        /// </summary>
        public const string OnlyLetterNumber = "custom[onlyLetterNumber]";
        /// <summary>
        /// 最多选取的项目数（用于Checkbox）
        /// </summary>
        /// <returns></returns>
        public static string MaxCheckbox(int i)
        {
            return string.Format("maxCheckbox[{0}]", i);
        }
        /// <summary>
        /// 最少选取的项目数（用于Checkbox）
        /// </summary>
        /// <returns></returns>
        public static string MinCheckbox(int i)
        {
            return string.Format("minCheckbox[{0}]", i);
        }
        /// <summary>
        /// 最少输入字符数
        /// </summary>
        /// <returns></returns>
        public static string MinSize(int i)
        {
            return string.Format("minSize[{0}]", i);
        }
        /// <summary>
        /// 最多输入字符数
        /// </summary>
        /// <returns></returns>
        public static string MaxSize(int i)
        {
            return string.Format("maxSize[{0}]", i);
        }
        /// <summary>
        /// 最小值（数值的最小值）
        /// </summary>
        /// <returns></returns>
        public static string Min(int i)
        {
            return string.Format("min[{0}]", i);
        }
        /// <summary>
        /// 最大值（数值的最大值）
        /// </summary>
        /// <returns></returns>
        public static string Max(int i)
        {
            return string.Format("max[{0}]", i);
        }
        /// <summary>
        /// 日期必需在 date 或 date 的将来。格式为 YYYY/MM/DD、YYYY/M/D、YYYY-MM-DD、YYYY-M-D 或 now。
        /// </summary>
        /// <returns></returns>
        public static string Past(DateTime dt)
        {
            return string.Format("past[{0}]", dt);
        }
        /// <summary>
        /// 日期必须在 data 或 date 的过去。
        /// </summary>
        /// <returns></returns>
        public static string Future(DateTime dt)
        {
            return string.Format("future[{0}]", dt);
        }

    }

    public static class ToHtmlStringHelper
    {
        public static IHtmlString AddTitle(this IHtmlString str, string tip, string description)
        {
            var strHtml = new StringBuilder(str.ToHtmlString());
            if (!string.IsNullOrEmpty(tip))
                strHtml.AppendFormat("<label>{0}</label>", tip);
            if (!string.IsNullOrEmpty(description))
                strHtml.AppendLine(string.Format("<span class=\"f_help\">{0}</span>", description));
            return new HtmlString(strHtml.ToString());
        }
    }

}