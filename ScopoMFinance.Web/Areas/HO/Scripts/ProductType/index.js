$(".__lnkAddEdit").on("click", function (e) {
    e.preventDefault();

    var url = $(this).attr("data-url");
    $("#product-type-modal").load(url);
});