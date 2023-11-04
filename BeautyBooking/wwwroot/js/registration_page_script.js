$('input[type="radio"]').change(function () {
    var selectedValue = $('input[name="gender"]:checked').val();
    $('.selectedGender').val(selectedValue);
});

var maskOptions = {
    mask: '+{38} (000)000-00-00',
    lazy: false
} 
var mask = new IMask($('#phone'), maskOptions);