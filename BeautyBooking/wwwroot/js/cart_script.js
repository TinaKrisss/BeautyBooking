let totalDuration = 0;
let totalPrice = 0;
let resultString;
let chooseMode = false;
var cartArray = [];

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

        cartArray.push($(this).attr("id"));
    }
    else {
        //remove from cart
        totalPrice -= parseInt($(this).find(".price").text());

        var durationInputValue = $(this).find(".service-duration").text().trim();
        totalDuration -= parseInt(durationInputValue.slice(0, -4)) || 0;

        var hours = Math.floor(totalDuration / 60);

        var minutes = totalDuration % 60;

        var resultString = hours + ":" + minutes;

        removeFromCart($(this).attr("id"));

        if (totalPrice <= 0) {
            $(".cart").css("display", "none");
            chooseMode = false;
        }
    }

    $("#total-sum").text(totalPrice);
    $("#total-time").text(resultString);
});

function removeFromCart(productId) {
    var index = cartArray.indexOf(productId);

    if (index !== -1) {
        cartArray.splice(index, 1);
    }
}

function saveCart() {
    sessionStorage.setItem('Cart', JSON.stringify(cartArray));
    sessionStorage.setItem('total-duration', $("#total-time").text());
    sessionStorage.setItem('total-price', $("#total-sum").text());
    sessionStorage.setItem('chooseMode', chooseMode);
}

$(document).ready(function () {
    var chooseMode = sessionStorage.getItem('chooseMode');

    if (chooseMode) {
        $(".btn-details").css("display", "none");
        $(".btn-choose-master").css("display", "block")
    }
    else {
        $(".btn-details").css("display", "block");
        $(".btn-choose-master").css("display", "none");
    }
});
