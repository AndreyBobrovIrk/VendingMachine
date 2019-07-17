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
        $('button#select_drink-' + data.List[i].Id).attr("disabled", !data.List[i].Available);
    }
}

function GetChange(data) {
    InsertCoin(0);
    alert("Ваша сдача: " + data + " руб.");
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

    $("button[id |= 'select_drink']").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/SelectDrink",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: $(this).attr('value') }),
                success: SelectDrink,
            });            
        }
    )

    $("button[id |= 'insert_coin']").click(
        function () {
            $.ajax({
                type: "GET",
                url: "/Drinks/InsertCoin",
                dataType: 'json',
                contentType: 'application/json',
                mimeType: 'application/json',
                data: ({ id: $(this).attr('value') }),
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
