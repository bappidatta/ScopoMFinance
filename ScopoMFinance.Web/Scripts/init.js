$(document).ready(function () {
    $('.datepicker').pickadate({
        format: 'd mmmm, yyyy',
        formatSubmit: 'MM/dd/yyyy',
        selectMonths: true,
        selectYears: true
    });
});