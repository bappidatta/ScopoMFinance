$(function () {
    $('#ProductId').on('change', function () {
        var $this = $(this);
        if ($this.val())
            $('#mappedListContainer').load('/HO/Product/ComponentMappedList/' + $this.val());
        else
            $('#mappedListContainer').html('');
    });

    if ($('#ProductId').val())
        $('#ProductId').change();
});