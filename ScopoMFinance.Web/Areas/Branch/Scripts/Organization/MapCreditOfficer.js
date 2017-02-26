$(function () {
    $('#CreditOfficerId').on('change', function () {
        var $this = $(this);
        if ($this.val())
            $('#mappedListContainer').load('/Branch/Organization/CreditOfficerMappedList/' + $this.val());
        else
            $('#mappedListContainer').html('');
    });

    if ($('#CreditOfficerId').val())
        $('#CreditOfficerId').change();
});