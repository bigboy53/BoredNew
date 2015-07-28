$(document).ready(function () {
    $('#login').show().animate({ opacity: 1 }, 2000);
    $('.logo').show().animate({ opacity: 1, top: '40%' }, 800, function() {
        $('.logo').show().delay(1200).animate({ opacity: 1, top: '12%' }, 300, function() {
            $('.formLogin').animate({ opacity: 1, left: '0' }, 300);
            $('.userbox').animate({ opacity: 0 }, 200).hide();
        });

    });
    $(".on_off_checkbox").iphoneStyle();
    $('.tip a ').tipsy({ gravity: 'sw' });
    $('.tip input').tipsy({ trigger: 'focus', gravity: 'w' });
});
$('.userload').click(function (e) {
    $('.formLogin').animate({ opacity: 1, left: '0' }, 300);
    $('.userbox').animate({ opacity: 0 }, 200, function () {
        $('.userbox').hide();
    });
});

$('#but_login').click(function (e) {
    if (document.formLogin.username.value == "" || document.formLogin.password.value == "") {
        showError("请输入用户名和密码");
        $('.inner').jrumble({ x: 4, y: 0, rotation: 0 });
        $('.inner').trigger('startRumble');
        setTimeout('$(".inner").trigger("stopRumble")', 500);
        setTimeout('hideTop()', 5000);
        return false;
    }
    hideTop();
    Login();
});

$('#alertMessage').click(function () {
    hideTop();
});

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

function showSuccess(str) {
    $('#alertMessage').removeClass('error').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}

function hideTop() {
    $('#alertMessage').animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
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
function Login() {
    loading("请稍等...");
    var username = $("#username_id").val();
    var password = $("#password").val();
    $.ajax({
        type: "post",
        url: "/Home/LoginJ",
        data: { username: username, password: password },
        dataType: "json",
        cache: false,
        //beforeSend: function () {
        //    $("#login").animate({ opacity: 1, top: '49%' }, 200, function () {
        //        $('.userbox').show().animate({ opacity: 1 }, 500);
        //        $("#login").animate({ opacity: 0, top: '60%' }, 500, function () {
        //            $(this).fadeOut(200, function () {
        //                $(".text_success").slideDown();
        //                $("#successLogin").animate({ opacity: 1, height: "200px" }, 500);
        //            });
        //        });
        //    });
        //},
        success: function (data) {
            unloading();
            if (data.Result) {
                window.location.href = '/';
            } else {
                showError(data.Error);
            }
        },
        error: function (data) {
            unloading();
            showError(data.Error);
        }
    });
}