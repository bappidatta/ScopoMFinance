$(function () {
    $('#CreditOfficerId').on('change', function () {
        var $this = $(this);
        $('#mappedListContainer').load('/Branch/Organization/CreditOfficerMappedList/' + $this.val());
    });

    if ($('#CreditOfficerId').val())
        $('#CreditOfficerId').change();
});