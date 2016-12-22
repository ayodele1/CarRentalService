$(function () {
    console.log("yah");
    $(".pickupdate").datepicker();

    $(".returndate").datepicker();
    for (i = 0; i < 20; i++) {
        var cardid = "#card" + i;
        //$(cardid).flip({
        //    trigger:'hover'
        //})
    }
    //$("#card").flip({
    //    trigger: 'manual'
    //});

});
//$('#uservehicle').on('click', function () {
//    $(this).closest('.thumbnail').addClass('selectedVehicle');
//});

$('#carInventoryFilter').on('change', function (event) {
    var form = $(event.target).parents('form');
    form.submit();
});

$('.vehicleDetail').on('click', function () {
    var card_id = jQuery($(this).closest(".card")).attr("id");
    $(card_id).flip({
        axis: 'x',
        trigger: 'hover'
    });
});
