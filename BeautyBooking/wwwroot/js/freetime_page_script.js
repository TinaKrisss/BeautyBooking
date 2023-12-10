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
    var freeTimeId = $(".time-selected .time-item").attr("id");

    // Отправляем данные на сервер с помощью AJAX
    $.ajax({
        url: '/Records/Confirmation',
        method: 'POST',
        data: {
            cartData: cartData,
            freeTimeIdString: freeTimeId,
            masterIdString: masterId
        },

        success: function (response) {
            window.location.href = '/Records/Confirmation?recordId=' + response;
            sessionStorage.removeItem('Cart');
            sessionStorage.removeItem('chooseMode');
        },
        error: function (error) {
            // Обработка ошибок
            console.error('Произошла ошибка при отправке данных на сервер');
            
        }
    });
}