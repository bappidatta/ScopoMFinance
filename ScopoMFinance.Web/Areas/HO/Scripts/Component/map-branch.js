$(function () {
    $('#BranchId').on('change', function () {
        var $this = $(this);
        if ($this.val())
            $('#mappedListContainer').load('/HO/Component/ComponentMappedList/' + $this.val());
        else
            $('#mappedListContainer').html('');
    });

    if ($('#BranchId').val())
        $('#BranchId').change();
});