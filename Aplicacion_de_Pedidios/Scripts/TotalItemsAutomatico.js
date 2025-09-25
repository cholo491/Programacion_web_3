//update
function updateTotalItemsAutomatico() {
    var totalItems = 0;
    $('.item-row').each(function() {
        var quantity = parseFloat($(this).find('.item-quantity').val()) || 0;
        totalItems += quantity;
    });
    $('#total-items').text(totalItems);
}