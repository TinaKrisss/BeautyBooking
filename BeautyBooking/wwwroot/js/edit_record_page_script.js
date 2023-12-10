$('input[type="radio"]').change(function () {
    var selectedValue = $('input[name="status"]:checked').val();
    $('.selectedStatus').val(selectedValue);
});

$(document).ready(function () {
    if (!($("#userType").val() == "Master")) {
        toggleDateTimeInput();

        $('#notConfirmed, #confirmed, #done, #cancelled').change(function () {
            toggleDateTimeInput();
        });
    }
    else {
        $('#notConfirmed, #confirmed, #done, #cancelled').prop('disabled', true);
	}
});

function toggleDateTimeInput() {
    var isConfirmed = $('#notConfirmed').prop('checked');
    $('#datetimeinput').prop('readonly', !(isConfirmed));
}
