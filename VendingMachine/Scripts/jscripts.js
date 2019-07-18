function InsertCoin(data) {
    $('th#coins-total').html(data);

    GetAvailableDrinks();
}

function SelectDrink(data) {
    $('td#drink_count-' + data.Id).html(data.Count);
    $('th#coins-total').html(data.Coins);

    GetAvailableDrinks();
}

function UpdateAvailableDrinks(data) {
    for (i = 0; i < data.List.length; ++i) {
        $('input#select_drink-' + data.List[i].Id).attr("disabled", !data.List[i].Available);
    }
}

function UpdateDisabledCoins(data) {
    for (i = 0; i < data.List.length; ++i) {
        $('input#disable_coin-' + data.List[i].Id).attr("checked", data.List[i].Disabled);
        $('input#insert_coin-' + data.List[i].Id).attr("disabled", data.List[i].Disabled);
    }
}

function GetChange(data) {
    InsertCoin(0);

    var msg = "Ваша сдача: " + data + " руб\r\n";
    var coins = [1, 2, 5, 10];

    while (data > 0) {
        var i = coins.pop();
        var d = data / i;
        var count = Math.trunc(d);
        if (count == 0) {
            continue;
        }

        msg += "  " + i + " руб. " + count + "шт.\r\n";
        data -= count * i;
    }

    alert(msg);
}

function GetAvailableDrinks() {
    $.ajax({
        type: "GET",
        url: "/Drinks/GetAvailableDrinks",
        success: UpdateAvailableDrinks
    });
}

$(document).ready(function () {
    GetAvailableDrinks();

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

    $("input[id |= 'select_drink']").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/SelectDrink",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: $(this).attr('name') }),
                success: SelectDrink,
            });            
        }
    )

    $("input[id |= 'insert_coin']").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/InsertCoin",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: $(this).attr('name') }),
                success: InsertCoin,
            });
        }
    )

    $("a#get-change").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/GetChange",
                success: GetChange,
            });
        }
    )
});
