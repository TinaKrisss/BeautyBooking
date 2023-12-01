$(document).ready(function () {
    $(".free-date").click(function () {

        $(".free-date").removeClass("date-selected");
        $(".free-time").removeClass("time-selected");

        $(this).addClass("date-selected");

        var selectedDate = $(this).attr("id");

        $(".free-time").addClass("hidden");

        var matchingBlocks = $("." + selectedDate);

        matchingBlocks.removeClass("hidden");
    });

    $(".free-time").click(function () {
        $(".free-time").removeClass("time-selected");

        $(this).addClass("time-selected");
    });
});