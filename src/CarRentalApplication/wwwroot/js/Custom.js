$(function () {
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
    
    $('#showdetailsbtn').click(function () {
        $('.dashboard-reservations-list').slideToggle();
        console.log($("#showdetailsbtn").text());
        if ($("#showdetailsbtn").text() === "Show Details") {
            $("#showdetailsbtn").text("Hide Details");
        } else {
            $("#showdetailsbtn").text("Show Details");
        }

    });
});


