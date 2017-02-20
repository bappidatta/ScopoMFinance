$(".__lnkAddEdit").on("click", function (e) {
    e.preventDefault();

    var url = $(this).attr("data-url");
    $("#edit-employee-type").load(url);
});