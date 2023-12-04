let totalDuration = 0;
let totalPrice = 0;
let resultString;
let chooseMode = false;

$(".select-type").click(function () {
    chooseMode = true;
    $(".cart").css("display", "block");

    if (!$(this).hasClass("active")) {
        //add to cart
        totalPrice += parseInt($(this).find(".price").text());

        var durationInputValue = $(this).find(".service-duration").text().trim();

        totalDuration += parseInt(durationInputValue.slice(0, -4)) || 0;
        var hours = Math.floor(totalDuration / 60);

        var minutes = totalDuration % 60;

        resultString = hours + ":" + minutes;
    }
    else {
        //remove from cart
        totalPrice -= parseInt($(this).find(".price").text());

        var durationInputValue = $(this).find(".service-duration").text().trim();
        totalDuration -= parseInt(durationInputValue.slice(0, -4)) || 0;

        var hours = Math.floor(totalDuration / 60);

        var minutes = totalDuration % 60;

        var resultString = hours + ":" + minutes;

        if (totalPrice <= 0) {
            $(".cart").css("display", "none");
            chooseMode = false;
        }
    }

    $("#total-sum").text(totalPrice);
    $("#total-time").text(resultString);
});

function saveCart() {
    sessionStorage.setItem('chooseMode', chooseMode);
}
/*$(document).ready(function () {*/

//    var chooseMode = sessionStorage.getItem('chooseMode');
//    if (chooseMode) {
//        $(".master-btns").append('<a class="btn btn-outline-info btn-choose-master" asp-action="FreeTimeDetails" asp-route-id="@item.Id">Обрати</a>');
//        $(".master-btns .btn-details").remove();
//    }
//});