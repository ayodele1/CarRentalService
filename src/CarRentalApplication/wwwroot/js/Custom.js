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

    $('.vehicleDetails').on('click', function () {
        $(this).closest('.customThumbnail').find('.card').flip('toggle');
    });

});


