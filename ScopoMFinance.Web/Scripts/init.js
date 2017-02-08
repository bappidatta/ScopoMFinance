(function ($) {
    $(function () {
        $('.button-collapse').sideNav();

        $('select').material_select();

        $('.datepicker').pickadate({
            onSet: function (arg) {
                if ('select' in arg) { //prevent closing on selecting month/year
                    this.close();
                }
            },
            formatSubmit: 'yyyy/mm/dd',
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });

        $('#OpenDate').on('change', function () {
            if ($(this).val())
                $('span[for="OpenDate"]').hide();
            else
                $('span[for="OpenDate"]').show();
        })
    }); // end of document ready
})(jQuery); // end of jQuery name space