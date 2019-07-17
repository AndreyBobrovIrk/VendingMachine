function InsertCoins(data) {
    $('th#coins-count-' + data.Id).html(data.Count);
    $('th#coins-total').html(data.Total);
}

$(document).ready(function () {
    $("button[id |= 'select_drink']").click(
        function () {
            
        }
    )

    $("button[id |= 'insert_coin']").click(
        function () {
            var id = $(this).attr('value');
            $.ajax({
                type: "GET",
                url: "/Drinks/InsertCoin",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: id }),
                success: InsertCoins,
            });
        }
    )
});
