$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    var dateToday = new Date();
    $(".pickupdate").datepicker({ dateFormat: "yy-mm-dd", minDate: dateToday }).datepicker("setDate", "0");

    $(".returndate").datepicker({ dateFormat: "yy-mm-dd", minDate:dateToday }).datepicker("setDate", "0");

    $('.card').flip({
        trigger: 'manual'
    });

    $('#carInventoryFilter').on('change', function (event) {
        var form = $(event.target).parents('form');
        form.submit();
    });

    $('#carInventoryPropertyFilter').on('change', function (event) {
        var form = $(event.target).parents('form');
        form.submit();
    });

    $('.vehicleDetails').on('click', function () {
        $(this).closest('.customThumbnail').find('.card').flip('toggle');
    });

    $('#createaccount').change(function () {
        if ($('.accountCreation > .form-group').hasClass('disabledAccountReg')) {
            $('.accountCreation > .form-group').removeClass('disabledAccountReg');
        }
        else {
            $('.accountCreation > .form-group').addClass('disabledAccountReg');
        }
        $("#reservationRegPassword").prop("disabled", !$(this).is(':checked'));
        $("#reservationRegConfirmPassword").prop("disabled", !$(this).is(':checked'));

    });
    
    $('.dashboard-reservation-details').click(function () {
        var id = $(this).attr('id');
        console.log(id);
        console.log($('.dashboard-reservations-list').attr('id'));
        $('#' + id + '.dashboard-reservations-list').slideToggle();
        
        if ($(this).attr('value') === 'Show Details') {
            $(this).attr('value', 'Hide Details');
        } else {
            $(this).attr('value', 'Show Details');
        }

    });
});


