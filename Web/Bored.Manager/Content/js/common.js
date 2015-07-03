$(function () {
    LResize();
    $(window).resize(function () { LResize(); });
    $(window).scroll(function () { scrollmenu(); });

    // logout  
    $('.logout').live('click', function () {
        var str = "Logout";
        var overlay = "1";
        loading(str, overlay);
        setTimeout("unloading()", 1500);
        setTimeout("window.location.href='index.html'", 2000);
    });

    // checkbox,selectbox customInput 
    $('.ck,.chkbox,.checkAll ,input:radio').customInput();

    //select
    $('select').not("select.chzn-select,select[multiple],select#box1Storage,select#box2Storage").selectmenu({
        style: 'dropdown',
        transferClasses: true,
        width: null
    });
    $(".checkedShow").each(function (i, v) {
        var obj = $(this);
        $(this).iphoneStyle({
            checkedLabel: $(this).attr("ca"),
            uncheckedLabel: $(this).attr("uca"),
            labelWidth: $(this).attr("cw"),
            onChange: function () {
                var chek = obj.attr('checked');
                if (chek) {
                    eval(obj.attr("fun"));
                } else {
                }
            }
        });
    });

    
   
    /*Table中添加和修改功能*/
    $("#common_add").live('click', function () {
        var activeLoad = $(this).attr("name");
        var titleTabs = $(this).attr("original-title");
        showEdit(activeLoad, titleTabs);
    });
    //返回列表
    //$("#on_prev_pro").live('click', function () {
    //    //$("ul.tabs li:last").remove();
    //    //$("ul.tabs li").fadeIn();
    //    //$(".show_add").remove();
    //    //$('.onecolumn .content').css({ 'z-index': '', 'box-shadow': '', '-moz-box-shadow': '', '-webkit-box-shadow': '' });
    //    //$('#overlay').hide();
    //    //$('.load_page').show();
    //});
    /*END*/

    // table在各个浏览器中设置
    Common.setBrowser();

    // tabs
    $("ul.tabs li").fadeIn(400);
    $("ul.tabs li:first").addClass("active").fadeIn(400);
    $(".tab_content:first").fadeIn();
    $("ul.tabs li").live('click', function () {
        if ($(this).hasClass("active")) {
            return false;
        }
        $("ul.tabs li").removeClass("active");
        $(this).addClass("active");
        var activeTab = $(this).find("a").attr("href");
        $('.tab_content').fadeOut();
        $(activeTab).delay(400).fadeIn();
        ResetForm();
        return false;
    });

    // 按钮提示  tooltip
    $('.tip a ').tipsy({ gravity: 's', live: true });
    $('.ntip a ').tipsy({ gravity: 'n', live: true });
    $('.wtip a ').tipsy({ gravity: 'w', live: true });
    $('.etip a,.Base').tipsy({ gravity: 'e', live: true });
    $('.netip a ').tipsy({ gravity: 'ne', live: true });
    $('.nwtip a , .setting').tipsy({ gravity: 'nw', live: true });
    $('.swtip a,.iconmenu li a ').tipsy({ gravity: 'sw', live: true });
    $('.setip a ').tipsy({ gravity: 'se', live: true });
    $('.wtip input').tipsy({ trigger: 'focus', gravity: 'w', live: true });
    $('.etip input').tipsy({ trigger: 'focus', gravity: 'e', live: true });
    $('.iconBox, div.logout').tipsy({ gravity: 'ne', live: true });
    $('.flot-graph').tipsy({ gravity: 'ne', live: true, trigger: 'click', });

    // keyup过滤
    $('input.numericonly').autotab_magic().autotab_filter('numeric');
    $('input.textonly').autotab_magic().autotab_filter('text');
    $('input.alphaonly').autotab_magic().autotab_filter('alpha');
    $('input.regexonly').each(function(i, v) {
        var reg = $(v).attr("reg");
        $(v).autotab_magic().autotab_filter({ format: 'custom', pattern: reg });
    });
    
    $('input.alluppercase').autotab_magic().autotab_filter({ format: 'alphanumeric', uppercase: true });
    
    $("input.datetime").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });

    $(".rating_star").each(function (i, v) {
        var defultNumber = $(v).val();
        $(v).rating_star({
            rating_star_length: '5',
            rating_initial_value: defultNumber
        });
    });

    $(".DeleteAll").live("click", function () {
        var url = $(this).attr("url");
        var table = $(this).attr("table");
        var ids = "";
        var checkebox = $("#" + table).find("tbody tr td .chkbox:checked");
        if (checkebox.length <= 0) {
            Common.showError("对不起，请选中您要操作的记录！");
            return false;
        }
        checkebox.each(function (i, v) {
            ids += $(v).attr('value') + ",";
        });
        ids = ids.substr(0, ids.length - 1);
        $.confirm({
            'title': "提示",
            'message': "确定要删除？",
            'buttons': {
                '是': {
                    'class': 'special',
                    'action': function () {
                        Delete(url, ids, table);
                    }
                },
                '否': {
                    'class': ''
                }
            }
        });
        //Common.confirm("确定要删除？", "提示", "Delete('" + url + "','" + ids + "'," + table + ")");
    });
});

function ResetForm() {
    $('form').each(function (index) {
        var form_id = $('form:eq(' + index + ')').attr('id');
        if (form_id) {
            $('#' + form_id).get(0).reset();
            $('#' + form_id).validationEngine('hideAll');
            var editor = $('#' + form_id).find('#editor').attr('id');
            if (editor) {
                $('#editor').cleditor()[0].clear();
            }
        }
    });
}

//左边菜单适用分辨率
function LResize() {
    if ($.cookie("hide_")) {
        $('#hide_panel').show();
    }
    $("#shadowhead").show();
    if ($(window).width() <= 768) {
        $('body').addClass('nobg'); $('#hide_menu').hide(); $('#show_menu').hide();
        $('#shadowhead').css({ position: "fixed" });
        $(' .column_left, .column_right ,.grid2,.grid3,.grid1').css({ width: "100%", float: "none", padding: "0", marginBottom: "20px" });
        if ($.cookie("hide_") == '1') {
            $('#show_menu_icon').show(); $('#hide_menu_icon').hide();
            $('#left_menu,#load_menu').css({ left: "-213px" });
            $('#content').css({ marginLeft: "20px" });
        } else {
            $('#hide_menu_icon').show(); $('#show_menu_icon').hide();
            $('#left_menu,#load_menu').css({ left: "0px" }); $('#content').css({ marginLeft: "70px" });
            $('#main_menu').removeClass('main_menu').addClass('iconmenu');
            $('#main_menu li').each(function () {
                var title = $(this).find('b').text();
                $(this).find('a').attr('title', title);
            });
            $('#main_menu li a').find('b').hide();
            $('#main_menu li ').find('ul').hide();
        }
    }
    if ($(window).width() > 1024) {
        $('#main_menu').removeClass('iconmenu ').addClass('main_menu');
        $('#main_menu li a').find('b').show();
        $('#hide_menu_icon').hide(); $('#show_menu_icon').hide();
        $('.column_left,.column_right,.grid2').css({ width: "49%", float: "left" });
        $('.column_left').css({ 'padding-right': "1%" });
        $('.column_right').css({ 'padding-left': "1%" });
        $('.grid1').css({ width: "24%", float: "left" }); $('.grid3').css({ width: "74%", float: "left" });

        if ($.cookie("hide_") == '1') {
            $('#show_menu').show(); $('#hide_menu').hide(); $('body').addClass('nobg');
            $('#left_menu,#load_menu').css({ left: "-213px" });
            $('#content').css({ marginLeft: "40px" });
            $('#shadowhead').css({ position: "fixed" });
        } else {
            $('#hide_menu').show(); $('#show_menu').hide();
            $('#left_menu,#load_menu').css({ left: "0px" });
            $('#content').css({ marginLeft: "240px" });
            $('body').removeClass('nobg').addClass('dashborad');
            $('#shadowhead').css({ position: "absolute" });
        }

    } else {
        $(' .column_left, .column_right ,.grid2,.grid3,.grid1').css({ width: "100%", float: "none", padding: "0", marginBottom: "20px" });
    }
}

function scrollmenu() {
    if ($(window).scrollTop() >= 1) {
        $("#header").css("z-index", "50");
    } else {
        $("#header").css("z-index", "47");
    }
}

function DeleteSingle(url, ids, tableName) {
    $.confirm({
        'title': "提示",
        'message': "确定要删除？",
        'buttons': {
            '是': {
                'class': 'special',
                'action': function () {
                    Delete(url, ids, tableName);
                }
            },
            '否': {
                'class': ''
            }
        }
    });
    //Common.confirm("确定要删除？", "提示", "Delete('" + url + "','" + ids + "'," + tableName + ")");
}

function Delete(url, data, tableName) {
    Common.loading("正在处理...",1);
    $.ajax({
        url: url,
        data: { id: data },
        dataType: "json",
        type: "post",
        async: false, //同步
        success: function(rel) {
            Common.unloading();
            if (rel.Result) {
                Common.showSuccess("操作成功！");
            } else {
                Common.showError(rel.Msg);
            }
            var op = $('#' + tableName).data('_operation');
            op.PageIndex = 1;
            Ui.Table(op);
        },
        error: function(e) {
            Common.unloading();
            Common.showError("操作错误，请刷新重试！");
        }
    });
}

function Submit(formId) {
    $("#" + formId).submit();
}

function ImageInit(id) {
    $("#"+id).find('.albumImage').unbind("dblclick").bind("dblclick", function () {
        $(this).find("a[rel=glr]").fancybox({
            'showCloseButton': true,
            'centerOnScroll': true,
            'overlayOpacity': 0.8,
            'padding': 0
        });
        $("#Imagesortable").find('a').trigger('click');
    });
    $("#" + id).find('.picHolder').unbind("hover").unbind("mouseout").bind("hover", function () {
        $(this).find('.picTitle').fadeTo(200, 1);
    }).bind("mouseout", function () {
        $(this).find('.picTitle').fadeTo(200, 0);
    });
}


var Common = {
    loading: function loading(name, overlay) {
        $("#overlay").remove();
        $("#preloader").remove();
        $('body').append('<div id="overlay"></div><div id="preloader">' + name + '..</div>');
        if (overlay == 1) {
            //$('#overlay').css('opacity', 0.1).fadeIn(function () { $('#preloader').fadeIn(); });
            $('#overlay').css('opacity', 0.4).css("z-index","9999999").show();
            $('#preloader').show();
            return false;
        }
        $('#preloader').fadeIn();
    },
    unloading: function unloading(isOverPlay) {
        //$('#preloader').fadeOut('fast', function () { $('#overlay').fadeOut() });
        if (!isOverPlay) {
            $('#overlay').hide();
        } else {
            $('#overlay').css('opacity', 0.4).show();
        }
        $('#preloader').hide();
       
    },
    confirm: function (message, tip, okfunction, canclefunction) {
        message = message || "确定";
        tip = tip || "提示";
        $.confirm({
            'title': tip,
            'message': message,
            'buttons': {
                '是': {
                    'class': 'special',
                    'action': function() {
                        eval();
                    }
                },
                '否': {
                    'class': '',
                    'action': function() {
                        eval(canclefunction);
                    }
                }
            }
        });
    },
    showSuccess: function (message) {
        message = message || "操作成功！";
        showSuccess(message, 5000);
    },
    showInfo:function(message) {
        showSuccess(message, 5000);
    },
    showError:function(message) {
        message = message || "错误！";
        showError(message,5000);
    },
    showWarning:function(message) {
        message = message || "警告！";
        showWarning(message,5000);
    },
    setBrowser: function () {
        var mybrowser = navigator.userAgent;
        if (mybrowser.indexOf('MSIE') > 0) {
            $(function () {
                $('.formEl_b fieldset').css('padding-top', '0');
                $('div.section label small').css('font-size', '10px');
                $('div.section  div .select_box').css({ 'margin-left': '-5px' });
                $('.iPhoneCheckContainer label').css({ 'padding-top': '6px' });
                $('.uibutton').css({ 'padding-top': '6px' });
                $('.uibutton.icon:before').css({ 'top': '1px' });
                $('.dataTables_wrapper .dataTables_length ').css({ 'margin-bottom': '10px' });
            });
        }
        else if (mybrowser.indexOf('Firefox') > 0) {
            $(function () {
                $('.formEl_b fieldset  legend').css('margin-bottom', '0px');
                $('table .custom-checkbox label').css('left', '3px');
            });
        }
        else if (mybrowser.indexOf('Presto') > 0) {
            $('select').css('padding-top', '8px');
        }
        else if (mybrowser.indexOf('Chrome') > 0) {
            $(function () {
                $('div.tab_content  ul.uibutton-group').css('margin-top', '-40px');
                $('div.section  div .select_box').css({ 'margin-top': '0px', 'margin-left': '-2px' });
                $('select').css('padding', '6px');
                $('table .custom-checkbox label').css('left', '3px');
            });
        }
    },
    post: function (url, data, successFun, beforeFun, completeFun, errorFun) {
        $.ajax({
            url: url,
            type: "post",
            data: data,
            beforeSend: function (XMLHttpRequest) {
                if (typeof (beforeFun) != "undefined" && beforeFun != null) {
                    beforeFun(XMLHttpRequest);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (typeof (errorFun) != "undefined" && errorFun != null) {
                    errorFun(XMLHttpRequest, textStatus, errorThrown);
                }
                else {
                    showError("请求出错!",2000);
                }
            },
            complete: function (XMLHttpRequest, textStatus) {
                if (typeof (completeFun) != "undefined" && completeFun != null) {
                    completeFun(XMLHttpRequest, textStatus);
                }
            },
            success: function (result) {
                if (result!=null&&result.ValidLgoin == false) {
                    window.top.location.href = result.Url;
                }
                if (result != null && typeof (result.Error)!="undefined") {
                    console.info(result.Error);
                    showError("请求出错", 2000);
                }
                successFun(result);
            }
        });
    },
    postAsync: function (url, data, successFun, beforeFun, completeFun, errorFun) {
        $.ajax({
            url: url,
            type: "post",
            data: data,
            async: false,
            beforeSend: function (XMLHttpRequest) {
                if (typeof (beforeFun) != "undefined" && beforeFun != null) {
                    beforeFun(XMLHttpRequest);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (typeof (errorFun) != "undefined" && errorFun != null) {
                    errorFun(XMLHttpRequest, textStatus, errorThrown);
                }
                else {
                    showWarning("请求出错!", 2000);
                }
            },
            complete: function (XMLHttpRequest, textStatus) {
                if (typeof (completeFun) != "undefined" && completeFun != null) {
                    completeFun(XMLHttpRequest, textStatus);
                }
            },
            success: function (result) {
                if (result != null && result.ValidLgoin == false) {
                    window.top.location.href = result.Url;
                }
                if (result != null && typeof (result.Error) != "undefined") {
                    console.info(result.Error);
                    showError("请求出错", 2000);
                }
                successFun(result);
            }
        });
    }
}

function showEdit(activeLoad, titleTabs) {
    $("#overlay").remove();
    $('body').append('<div id="overlay"></div>');
    $('#overlay').css('opacity', 0.4).show();
    //var isUrl = $(this).attr("isUrl");
    $("ul.tabs li").hide();
    $("ul.tabs ").append('<li class="active"><a>' + titleTabs + '</a></li>');
    $("ul.tabs li:last").fadeIn();
    $('.onecolumn .content').css({ 'position': 'relative', 'z-index': '1001' });
    $(".load_page").hide();
    var showAdd = '<div class="show_add"></div>';
    var listtable = $("#common_add").parents(".load_page");
    if (typeof (listtable) != "undefined") {
        listtable.after(showAdd);
    } else {
        Common.showInfo("出错了！");
        return false;
    }
    $(".show_add").load(activeLoad);
}

/*提示消息*/
function showError(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('success info warning').addClass('error').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('error').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showSuccess(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error info warning').addClass('success').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('success').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showWarning(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error success  info').addClass('warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showInfo(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error success  warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
// hide notify  Message with click
$('#alertMessage').live('click', function () {
    $(this).stop(true, true).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
});
/*提示消息END*/

function checkNumber(obj) {
    var _reg = /^\d+$/;
    if (!_reg.test($(obj).val())) {
        return false;
    }
    return true;
}
function _OnKeyUp(_this) {
    var val = $(_this).val();
    var reg1 = /^\d+$/;
    if (isNaN(val) || val.match(reg1) == null) {
        $(_this).val($(_this).val().replace(/\D/g, ''));
    }
}

function PrevList(tableId) {
    $("ul.tabs li:last").remove();
    $("ul.tabs li").fadeIn();
    $(".show_add").remove();
    $('.onecolumn .content').css({ 'z-index': '', 'box-shadow': '', '-moz-box-shadow': '', '-webkit-box-shadow': '' });
    $('.load_page').show();
    $("#" + tableId).validationEngine("hideAll");
    Ui.ReloadTable(tableId);
    //dataTable.ReloadTable(tableId);
}

function initControl(inputId) {
    $('#' + inputId).find(".ck,.chkbox,.checkAll ,input:radio").customInput();
    
}

function initInput(formId) {
    $("#" + formId).find(".ck,.chkbox,.checkAll ,input:radio").customInput();
    $("#" + formId).find(".chzn-select").chosen();
    $("#" + formId).find(".checkedShow").each(function (i, v) {
        var obj = $(this);
        $(this).iphoneStyle({
            checkedLabel: $(this).attr("ca"),
            uncheckedLabel: $(this).attr("uca"),
            labelWidth: $(this).attr("cw"),
            onChange: function () {
                var chek = obj.attr('checked');
                if (chek) {
                    eval(obj.attr("fun"));
                } else {
                }
            }
        });
    });
}