$('input[type="radio"]').change(function () {
    var selectedValue = $('input[name="status"]:checked').val();
    $('.selectedStatus').val(selectedValue);
});

$(document).ready(function () {
    toggleDateTimeInput();

    $('#notConfirmed, #confirmed, #done, #cancelled').change(function () {
        toggleDateTimeInput();
    });
});

function toggleDateTimeInput() {
    var isConfirmed = $('#notConfirmed').prop('checked');
    $('#datetimeinput').prop('disabled', !(isConfirmed));
}
