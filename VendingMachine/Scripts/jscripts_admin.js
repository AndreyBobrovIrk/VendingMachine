function UpdateDisabledCoins(data) {
    for (i = 0; i < data.List.length; ++i) {
        $('input#disable_coin-' + data.List[i].Id).attr("checked", data.List[i].Disabled);
    }
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Drinks/GetDisabledCoins",
        success: UpdateDisabledCoins
    });

    $("input[id |= 'disable_coin']").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/DisableCoin",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: $(this).attr('value') }),
            });
        }
    )
});
