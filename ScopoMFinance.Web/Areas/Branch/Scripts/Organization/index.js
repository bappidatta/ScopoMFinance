$(function () {
    $(".__dialog").on("click", function (e) {
        e.preventDefault();

        var url = $(this).attr("data-url");

        $("#dialog-container").load(url);
    });
});