$(function () {
    if ($.cookie("pagersize") != null) {
        $("#selectPagerSize").val($.cookie("pagersize"));
    }
    else {
        $("#selectPagerSize").val(50);
    }
    $('#selectPagerSize').material_select();
    $("#selectPagerSize").change(function () {
        $.cookie("pagersize", $(this).val(), { path: "/" });

        window.location = window.location.href.replace(/(index=)([0-9]*)/, 'index=0');
    });
});