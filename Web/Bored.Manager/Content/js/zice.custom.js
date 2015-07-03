$.fn.imgdata = function (key) {
    return this.find('.dataImg li:eq(' + key + ')').text();
}
$.fn.hdata = function (key) {
    return this.find('.dataSet li:eq(' + key + ')').text();
}

$(function () {
    LResize();
    $(window).resize(function () { LResize(); });
    $(window).scroll(function () { scrollmenu(); });

    // submit form 
    $('a.submit_form').live('click', function() {
        var form_id = $(this).parents('form').attr('id');
        $("#" + form_id).submit();
    });

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
    // mutiselection
    $(".chzn-select").chosen();

    // checkbox初始化有样式的
    $(".checkedShow").each(function (i, v) {
        var obj = $(this);
        $(this).iphoneStyle({
            checkedLabel: $(this).attr("ca"),
            uncheckedLabel: $(this).attr("uca"),
            labelWidth: $(this).attr("cw"),
            onChange: function() {
                var chek = obj.attr('checked');
                if (chek) {
                    eval(obj.attr("fun"));
                } else {
                }
            }
        });
    });
   
    //checkbox  All in Table
    $(".checkAll").live('click', function () {
        var table = $(this).parents('table').attr('id');
        var checkedStatus = this.checked;
        var id = this.id;
        $("table#" + table + " tbody tr td:first-child input:checkbox").each(function () {
            this.checked = checkedStatus;
            if (this.checked) {
                $(this).attr('checked', $('.' + id).is(':checked'));
                $('label[for=' + $(this).attr('id') + ']').addClass('checked ');
            } else {
                $(this).attr('checked', $('.' + id).is(''));
                $('label[for=' + $(this).attr('id') + ']').removeClass('checked ');
            }
        });
    });
    /*Table中添加和修改功能*/
    $(".on_load").live('click', function () {
        $('body').append('<div id="overlay"></div>');
        $('#overlay').css('opacity', 0.4).fadeIn(400);
        var activeLoad = $(this).attr("name");
        var titleTabs = $(this).attr("title");
        $("ul.tabs li").hide();
        //$('ul.tabs li').each(function (index) {
        //    var activeTab = $('ul.tabs li:eq(' + index + ')').find("a").attr("href");
        //    if (activeTab == activeLoad) {
        //        $("ul.tabs ").append('<li class=active><a>' + titleTabs + '</a></li>');
        //        $("ul.tabs li:last").fadeIn();
        //    }
        //});
        $("ul.tabs ").append('<li class=active><a>' + titleTabs + '</a></li>');
        $("ul.tabs li:last").fadeIn();
        $('.onecolumn .content').css({ 'position': 'relative', 'z-index': '1001' });
        $(".load_page").hide();
        $('.show_add').show();
    });
    //返回列表
    $(".on_prev").live('click', function () {
        $("ul.tabs li:last").remove();
        $("ul.tabs li").fadeIn();
        //var pageLoad = $(this).attr("rel");
        var activeLoad = $(this).attr("name");
        $(".show_add, .show_edit").hide();
        $(".show_edit").html('').hide();
        $(activeLoad).fadeIn();
        $(' .load_page').fadeIn(400, function () {
            $('#overlay').fadeOut(function () {
                $('.onecolumn .content').delay(500).css({ 'z-index': '', 'box-shadow': '', '-moz-box-shadow': '', '-webkit-box-shadow': '' });
            });
        });
        ResetForm();
        $("#dd").validationEngine("attar", {
            ignore:"hidden"
        });
    });
    /*END*/

    // table在各个浏览器中设置
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
    if (mybrowser.indexOf('Firefox') > 0) {
        $(function () {
            $('.formEl_b fieldset  legend').css('margin-bottom', '0px');
            $('table .custom-checkbox label').css('left', '3px');
        });
    }
    if (mybrowser.indexOf('Presto') > 0) {
        $('select').css('padding-top', '8px');
    }
    if (mybrowser.indexOf('Chrome') > 0) {
        $(function () {
            $('div.tab_content  ul.uibutton-group').css('margin-top', '-40px');
            $('div.section  div .select_box').css({ 'margin-top': '0px', 'margin-left': '-2px' });
            $('select').css('padding', '6px');
            $('table .custom-checkbox label').css('left', '3px');
        });
    }
    if (mybrowser.indexOf('Safari') > 0) { }


    // tabs
    $("ul.tabs li").fadeIn(400);
    $("ul.tabs li:first").addClass("active").fadeIn(400);
    $(".tab_content:first").fadeIn();
    $("ul.tabs li").live('click', function () {
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

    //dataTable
    $('#abc').dataTable({
        //"aLengthMenu": [[50,100,500,1000,10000], [50,100,500,1000,10000]],//定义每页显示数据数量
        "iScrollLoadGap":400,//用于指定当DataTable设置为滚动时，最多可以一屏显示多少条数据
        //"bScrollCollapse": true,
        //"bDestroy": true,
        //"bSortCellsTop": true,
        //"bInfo": true,// Showing 1 to 10 of 23 entries 总记录数没也显示多少等信息
        "bWidth": true,
        "bServerSide": true,//服务端处理分页
        "sAjaxSource": "/Content/arrays.json",
        'bPaginate': true,  //是否分页。
        "bProcessing": true, //当datatable获取数据时候是否显示正在处理提示信息。
        'bFilter': false,  //是否使用内置的过滤功能
        "bSort": false, //开关，是否让各列具有按列排序功能
        'bLengthChange': true, //是否允许自定义每页显示条数.
        //'iDisplayLength':13, //每页显示10条记录
        "sPaginationType": "full_numbers", //分页样式   full_numbers
        "aoColumns": [
            //{ "sTitle": "编号", "sClass": "center" },
            //{ "sTitle": "城市名称", "sClass": "center" },
            //{ "sTitle": "邮政编码", "sClass": "center" },
            //{
            //    "sTitle": "操作",
            //    "sClass": "center",
            //    "fnRender": function (obj) {
            //        return '<a href=\"Details/' + obj.aData[0] + '\">查看详情</a>  <input tag=\"' + obj.aData[0] + '\" type=\"checkbox\" name=\"name\" />';
            //    }
            //}
            { "sTitle": "编号","sClass": "center", "mDataProp": "Trident" }
        ]
    });

    $('#sss').dataTable({
        'iScrollLoadGap': 400,
        'bWidth': true,
        'bServerSide': true,
        'sAjaxSource': '/Content/arrays.json',
        'bPaginate': true,
        'bProcessing': true,
        'bFilter': false, // 搜索栏
        'bSort': false, // 排序
        'bLengthChange': true,
        'sPaginationType': 'full_numbers',
    });


    // form validationEngine
    $('form#validation').validationEngine();
    $('form#validation_demo').validationEngine();

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
});


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

function loading(name, overlay) {
    $('body').append('<div id="overlay"></div><div id="preloader">' + name + '..</div>');
    if (overlay == 1) {
        $('#overlay').css('opacity', 0.1).fadeIn(function () { $('#preloader').fadeIn(); });
        return false;
    }
    $('#preloader').fadeIn();
}
function unloading() {
    $('#preloader').fadeOut('fast', function () { $('#overlay').fadeOut(); });
}


