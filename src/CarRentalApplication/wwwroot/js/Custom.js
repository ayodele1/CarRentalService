$(function () {
    console.log("yah");
    $(".pickupdate").datepicker();

    $(".returndate").datepicker();

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
});


