$('input[type="radio"]').change(function () {
    var selectedValue = $('input[name="gender"]:checked').val();
    $('.selectedGender').val(selectedValue);
});

//Mask for phone input
//$('#phone').on('focus', function (e) {
//    var x = e.target.value.replace(/\D/g, '').match(/(\d{3})(\d{3})(\d{4})/);
//    e.target.value = '(' + x[1] + ') ' + x[2] + '-' + x[3];
//});
