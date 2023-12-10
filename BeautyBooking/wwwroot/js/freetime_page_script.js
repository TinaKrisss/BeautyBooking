$(document).ready(function () {
    $(".free-date").click(function () {

        $(".free-date").removeClass("date-selected");
        $(".free-time").removeClass("time-selected");
        $(".edit-btn-container").prop("disabled", true);

        $(this).addClass("date-selected");

        var selectedDate = $(this).attr("id");

        $(".free-time").addClass("hidden");

        var matchingBlocks = $("." + selectedDate);

        matchingBlocks.removeClass("hidden");
    });

    $(".free-time").click(function () {
        $(".free-time").removeClass("time-selected");
        $(".edit-btn-container").prop("disabled", false);
        $(this).addClass("time-selected");
    });
});

function saveSelection(masterId) {
    var cartData = sessionStorage.getItem('Cart');
    var totalDuration = sessionStorage.getItem('total-duration');
    var totalPrice = sessionStorage.getItem('total-price');
    var freeTimeId = $(".time-selected .time-item").attr("id");

    // Отправляем данные на сервер с помощью AJAX
    $.ajax({
        url: '/Records/Confirmation',
        method: 'POST',
        data: {
            cartData: cartData,
            totalDuration: totalDuration,
            totalPrice: totalPrice,
            freeTimeIdString: freeTimeId,
            masterIdString: masterId
        },

        success: function (response) {
            console.log(response);
            // Обработка успешного ответа от сервера
            console.log('Данные успешно отправлены на сервер');
            window.location.href = '/Records/Confirmation';
            console.log(response);
            // Добавьте здесь код, который нужно выполнить после успешной отправки данных на сервер
        },
        error: function (error) {
            // Обработка ошибок
            console.error('Произошла ошибка при отправке данных на сервер');
            
        }
    });
    //sessionStorage.setItem("freeTimeId", $(".time-selected .time-item").attr("id"));
}