var Ui = {
    Table: function (operation) {
        var id = operation.Id;//创建表格的ID
        var url = operation.Url;//请求路径
        var pageIndex = operation.PageIndex || 1;//页号
        var pageSize = operation.PageSize || 10;//页显示数量
        var searchParam = operation.SearchParam||[];//查询参数的对象
        var colums = operation.Colums;//列
        var showCheck = operation.ShowCheck || true;//是否有全选
        var checkField = operation.CheckField || 'ID';//选择的字段
        var selectAllId = operation.SelectAllId;//全选按钮ID

        $('#' + id).data("_operation", operation);
        Common.post(url + "?page=" + pageIndex + "&rows=" + pageSize, searchParam, function (data) {
            var table = '';
            table += '<table  width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">';
            //--
            table += '<thead><tr class="odd_bg">';
            if (showCheck) {
                table += '<th width="1%"><input type=\"checkbox\" onclick="Ui.SelectAllBtn(this,\'' + id + '\')" id="' + selectAllId + '" class=\"checkAll\"/></th>';
            }
            colums.forEach(function (col) {
                table += '<th width="' + col.Width + '%">' + col.Title + '</th>';
            });
            table += '</tr></thead>';
            if (data.Data != null && data.Data != undefined && data.Data.length>0) {
                data.Data.forEach(function(dataItem) {
                    table += '<tr style="cursor: pointer;" >';
                    //增加行(tr)点击事件和鼠标样式
                    if (showCheck) {
                        table += '<td align="center">';
                        var sidVal = eval("(dataItem." + checkField + ")");
                        table += '<input type="checkbox" name="checkbox_' + id + '" class="chkbox"  id="check' + sidVal + '" value="' + sidVal + '"/>';
                        table += '</td>';
                    }
                    colums.forEach(function(col) {
                        var colValue = eval("(dataItem." + col.Field + ")");
                        col.Formater = col.Formater || function(item, value) {
                            return value;
                        };
                        if (typeof (colValue) == "string") {
                            //替换掉html标签标识
                            colValue = colValue.replace(/>/g, "\>").replace(/</g, "\<").replace(/"/g, "''").replace(/^\s+|\s+$/g, "");
                        }
                        if (typeof (col.ShowNumbers) != "undefined") {
                            table += '<td align="center" title="' + colValue + '">';
                            if (colValue != null && colValue.length > col.ShowNumbers) {
                                colValue = colValue.substring(0, col.ShowNumbers) + "...";
                            }
                        } else {
                            table += '<td align="center" >';
                        }
                        table += col.Formater(dataItem, colValue);
                        table += '</td>';
                    });
                    table += '</tr>';
                });
            } else {
                var col = colums.length;
                if (showCheck) {
                    col++;
                }
                table += '<tr><td colspan="' + col + '">无数据</td></tr>';
            }
            //--
            table += '</table>';
            $('#' + id).html(table);
            var pl = '<div class="line20"></div>';
            pl += '<div class="pagelist"><div class="l-btns"><span>显示</span>';
            //onchange="var pz=$(this).val();var op=$(\'#' + id + '\').data(\'_operation\');op.PageSize=pz;op.PageIndex=1;Ui.Table(op);"
            pl += '<input name="txtPageNum" type="text" value="' + pageSize + '" class="pagenum" onkeyup="_OnKeyUp(this)" onkeydown="return checkNumber(this);">';
            pl += '<span>条/页</span>';
            pl += '<div class="pagenum" style="width: 1px;padding: 0px;border-right: none;"/><span>共' + data.DataCount + '记录</span>';
            pl += '</div>';
            pl += '<div id="PageContent" class="paging_full_numbers">';
            var ps = GetShowPageIndexs(data.PageCount, data.PageIndex);
            var pi = 0;
            if (pageIndex == 1) {
                pl += '<a href="javascript:void(0);" class="first paginate_button paginate_button_disabled">«第一页</a>';
            } else {
                pl += '<a href="javascript:void(0);" class="first paginate_button paginate_button_disabled" onclick="var op=$(\'#' + id + '\').data(\'_operation\');op.PageIndex=1;Ui.Table(op);">«第一页</a>';
            }
            
            for (var i = 0; i < ps.length; i++) {
                 pi = ps[i];
                if (pi == data.PageIndex) {
                    pl += '<a href="javascript:void(0);" class="paginate_active current">' + pi + '</a>';
                }
                else {
                    pl += '<a tabindex="0" class="paginate_button" onclick="var op=$(\'#' + id + '\').data(\'_operation\');op.PageIndex=' + pi + ';Ui.Table(op);">' + pi + '</a>';
                }
            }
            if (pageIndex == data.PageCount) {
                pl += '<a href="javascript:void(0);" class="next paginate_button">最后页»</a>';
            } else {
                pl += '<a href="javascript:void(0);" class="first paginate_button paginate_button_disabled" onclick="var op=$(\'#' + id + '\').data(\'_operation\');op.PageIndex=' + data.PageCount + ';Ui.Table(op);">最后页»</a>';
            }
            pl += ' </div>';
            pl += '</div>';
            $('#' + id).append(pl);
            $('#' + id).find('input[name="txtPageNum"]').bind('keypress', function (event) {
                if (typeof (event.which) == "undefined" && event.keyCode == 13 || event.which == 13) {
                    var pz = $(this).val();
                    var op = $('#' + id).data('_operation');
                    op.PageSize = pz;
                    op.PageIndex = 1;
                    Ui.Table(op);
                }
            });
            $('#' + id).find('input[name="txtPageNum"]').change(function () {
                var pz = $(this).val();
                var op = $('#' + id).data('_operation');
                op.PageSize = pz;
                op.PageIndex = 1;
                Ui.Table(op);
            });
        }, function () {
            Common.loading("正在加载...",1);
        }, function () {
            Common.unloading();
            initControl(id);
        });
    },
    //查询按钮
    /*
    id:ID
    url:查询请求URL
    tableId:查询对应的表格ID
    */
    SearchBtn: function (id, tableId) {
        $('#' + id).click(function () {
            var pz = $('#' + tableId).find('select[selected]').first().val();
            //---组装查询对象开始
            var json = {};
            var flag = 0;
            $(this).parent().find("input").each(function (i, ele) {
                var objName = $(ele).attr("name");
                var val = $(ele).val();
                json[objName] = val;
            });
            $(this).parent().find("select").each(function (i, ele) {
                var objName = $(ele).attr("name");
                var val = $(ele).val();
                json[objName] = val;
            });
            //--组装查询对象结束
            var op = $('#' + tableId).data('_operation');
            op.SearchParam = json;
            op.PageIndex = 1;
            Ui.Table(op);
        });
    },
    /*
    选择全部按钮
    id:ID
    tableId:查询对应的表格ID
    */
    SelectAllBtn: function (obj, tableId) {
        var chkobj = $(obj);
        chkobj.click(function () {
            var table = $("#" + tableId);
            var checkedStatus = this.checked;
            table.find("tbody tr td:first-child input:checkbox").each(function () {
                this.checked = checkedStatus;
                if (this.checked) {
                    $(this).attr('checked', chkobj.is(':checked'));
                    $('label[for=' + $(this).attr('id') + ']').addClass('checked ');
                } else {
                    $(this).attr('checked', chkobj.is(''));
                    $('label[for=' + $(this).attr('id') + ']').removeClass('checked ');
                }
            });

        });
       
    },
    /*
    获取指定容器内的所有表单控件数据
    formId:表单ID
    */
    FormData: function (formId) {
        var formData = {};
        $.each($("#" + formId).find('input'), function (i, input) {
            switch (input.type) {
                case 'hidden':
                case 'text':
                    if (typeof (input.name) == "undefined" || input.name ==null|| input.name == "") {
                        break;
                    }
                    formData[input.name] = $(input).val();
                    break;
                case 'password':
                    if ($(input).val() == "") {
                        formData[input.name] = "";
                    }
                    else {
                        formData[input.name] = $(input).val();
                    }
                    break;
                case 'checkbox':
                    if ($(input).prop("checked")) {
                        formData[input.name] = true;
                    }
                    else {
                        formData[input.name] = false;
                    }
                    break;
            }
        });
        $.each($("#" + formId).find('select'), function (index, select) {
            if (select.multiple) {
                var data = $(select).val();
                if (data != null && data.length>0) {
                    var value = "";
                    for (var i = 0; i < data.length; i++) {
                        value += data[i] + ",";
                    }
                    formData[select.name] = value.substr(0, value.length - 1);
                }
                else { formData[select.name] = ""; }
            } else {
                formData[select.name] = $(select).val();
            }
        });
        $.each($("#" + formId).find("textarea:not([name='editorValue'])"), function (i, txt) {
            formData[txt.name] = $(txt).val();
        });
        $.each($("#" + formId).find('.udeitor_value'), function (i, txt) {
            var id = $(txt).attr("id");
            formData[id] = UE.getEditor(id).getContent();
        });
        return formData;
    },
    InitForm: function (formId, opertationType, url, paramValue, addUrl, editUrl, succFun, otherFunc) {
        var udes = [];
        $("#" + formId).find('textarea[ud="true"]').each(function (i, ele) {
            var ud = UE.getEditor($(ele).attr("name"), { initialFrameWidth: 800 });
            udes.push(ud);
        });
        if (opertationType == "edit") {
            Common.post(url, { Id: paramValue }, function (formData) {
                $.each(formData, function (name, value) {
                    var inputs = $("#" + formId).find('input[name="' + name + '"]');
                    var selects = $("#" + formId).find('select[name="' + name + '"]');
                    var textareas = $("#" + formId).find('textarea[name="' + name + '"]');
                    //var ueditor = UE.getEditor(name);
                    if (inputs.length > 0)
                        switch (inputs[0].type) {
                            case 'hidden':
                            case 'text':
                                var rp = $(inputs[0]).attr('rp');
                                if (typeof (rp) == "undefined") {
                                    $(inputs[0]).val(formData[name]);
                                } else {
                                    $(inputs[0]).val(formData[name].toString().replace(rp,''));
                                }
                                var emp = $(inputs[0]).attr('data-empty');
                                if (typeof (emp) == "undefined") {
                                    $(inputs[0]).val(formData[name]);
                                } else {
                                    if (parseFloat(formData[name]) > 0) {
                                        $(inputs[0]).val(formData[name]);
                                    } else {
                                        $(inputs[0]).val("");
                                    }
                                }
                                break;
                            case 'password':
                                //$(inputs[0]).val(formData[name]);
                                break;
                            case 'checkbox':
                                $(inputs[0]).prop("checked", formData[name]);
                                break;
                        }
                    else if (selects.length > 0) {
                        if (selects[0].multiple) {
                            if (formData[name] != null && formData[name] != "") {
                                var data = formData[name].split(',');
                                for (var i = 0; i < data.length; i++) {
                                    $(selects[0]).find("option[value=" + data[i] + "]").attr("selected", "selected");
                                }
                            }
                        } else {
                            $(selects[0]).val(formData[name]);
                        }
                    }
                    else if (textareas.length > 0) {
                        if (typeof (formData[name]) != "undefined" && formData[name] != null)
                            $(textareas[0]).val(formData[name].toString());
                        udes.forEach(function (ele) {
                            if (ele.key == textareas[0].name) {
                                ele.ready(function () {
                                    ////设置编辑器的内容
                                    ele.setContent(formData[name].toString());
                                });
                            }
                        });
                    }
                    else if (typeof(ueditor) != "undefined") {
                        UE.getEditor(name).setContent(formData[name]);
                    }
                });
                //初始化表单验证
                if (otherFunc) {
                    otherFunc(formData);
                }
                initInput(formId);
            });
            
        }
        else if (opertationType == "add") {
            initInput(formId);
        }
        else {
            Common.showError("不明错误，请刷新重试！");
        }
    },
    ReloadTable: function (tableId) {
        var op = $('#' + tableId).data('_operation');
        op.PageIndex = 1;
        Ui.Table(op);
    }

};
//通过总页数和当前页取6个显示页码
/*
pageCount:页总数
pageIndex:当前页
*/
function GetShowPageIndexs(pageCount, pageIndex) {
    var showNum = 11;
    var pNum = (showNum - 1) / 2;
    var ps = [];
    if (pageCount > showNum) {
        if ((pageIndex - pNum) <= 0 && (pageIndex + pNum) < pageCount) {

            for (var i = 1; i <= showNum; i++) {
                ps.push(i);
            }
        } else if ((pageIndex + pNum) >= pageCount && (pageIndex - pNum) >= 0) {
            for (var i = (showNum - 1) ; i >= 0; i--) {
                ps.push(pageCount - i);
            }
        } else {
            for (var i = pNum; i > 0; i--) {
                ps.push((pageIndex - i));
            }
            ps.push(pageIndex);
            for (var i = 1; i <= pNum; i++) {
                ps.push(pageIndex + i);
            }
        }
    } else {
        for (var i = 1; i <= pageCount; i++) {
            ps.push(i);
        }
    }
    return ps;
}

//节点点击事件 单选
function onClick(e, treeId, treeNode) {
    var z = treeNode.id;
    var hidId = $("#" + treeId).attr("hidId");
    //$("#" + TreeValue).val(z);
    $("#" + hidId).val(z);
    var selId = $("#" + treeId).attr("inputId");

    $("#" + selId).focus();
    //$("#citySel").focus();
    //var cityObj = $("#citySel");
    var cityObj = $("#" + selId);
    $(cityObj).val(treeNode.name);
    //cityObj.attr("value", treeNode.name);
}

function onBodyDown(event) {
    var Id = event.target.id;
    var index = Id.indexOf("_");
    var mark = Id.substring(6, index);


    if (!(event.target.id == ("menuBtn" + mark) || event.target.id == ("menuContent" + mark) || $(event.target).parents("#menuContent" + mark).length > 0)) {
        $("body").find("div[IsTreeContent='true']").each(function () {
            //console.info("aaa");
            var markId = $(this).attr("id");

            var marks = markId.substring(11, markId.length);

            hideMenu(marks);
        });
    }
}


function hideMenu(mark) {
    //console.info(mark);
    $("#menuContent" + mark).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
//treetable删除节点时的方法
function TreeNodeDel(delUrl, nodeId,tableDivId) {
    $.dialog.confirm('确认是否删除', function () {
        ShowLoad();
        CustomPost(delUrl, { Fields: nodeId }, function (data) {
            if (data == "True" || data == "true" || data == true) {
                //$('#' + nodeId).remove();
                var op = $("#" + tableDivId).data("_operation");
                Ui.TreeTable(op);
                RightButtomAlert("删除成功!");
            } HideLoad();
        });
    });
}