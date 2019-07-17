function InsertCoins(data) {
    $('th#coins-count-' + data.Id).html(data.Count);
    $('th#coins-total').html(data.Total);
}
