$(".__lnkAddEdit").on("click", function (e) {
    e.preventDefault();

    var url = $(this).attr("data-url");
    $("#employee-type-modal").load(url);
});

$(".__lnkDelete").on("click", function (e) {
    e.preventDefault();

    var url = $(this).attr("data-url");
    $("#employee-type-modal").load(url);
});