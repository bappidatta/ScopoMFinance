$(".__lnkDelete").on("click", function (e) {
    e.preventDefault();

    var url = $(this).attr("data-url");
    $("#delete-dialog-container").load(url);
});