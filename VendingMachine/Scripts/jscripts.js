function InsertCoin(data) {
    $('th#coins-count-' + data.Id).html(data.Count);
    $('th#coins-total').html(data.Total);

    GetCoinsLimit();
    GetAvailableDrinks();
}

function SelectDrink(data) {
    $('th#selected_drink-' + data.Id).html("(" + data.Count + ")");

    GetDrinkLimit(data.Id);
    GetAvailableDrinks();
    GetCoinsLimit();
}

function UpdateAvailableDrinks(data) {
    for (i = 0; i < data.List.length; ++i) {
        $('button#select_drink-' + data.List[i].Id).attr("disabled", !data.List[i].Available);
    }
}

function UpdateCoinsLimit(data) {
    $('th#coins-limit').html(data);
}

function UpdateDrinkLimit(data) {
    $('td#drink_limit-' + data.Id).html(data.Count);
}

function GetAvailableDrinks() {
    $.ajax({
        type: "GET",
        url: "/Drinks/GetAvailableDrinks",
        success: UpdateAvailableDrinks
    });
}

function GetCoinsLimit() {
    $.ajax({
        type: "GET",
        url: "/Drinks/GetCoinsLimit",
        success: UpdateCoinsLimit
    });
}

function GetDrinkLimit(id) {
    $.ajax({
        type: "GET",
        url: "/Drinks/GetDrinkLimit",
        dataType: 'json',
        contentType: 'application/json',
        data: ({ id: id }),
        success: UpdateDrinkLimit
    });
}

function ConfirmOrder(data)
{
    var msg = "Ваш заказ:";

    alert(msg);
}

$(document).ready(function () {
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
});
